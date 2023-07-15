var app = angular.module('app', []);

app.controller('HomeController', function ($scope, $http ) {
    $scope.loginButtonText = "Login";
    $scope.forgotPasswordText = "Submit";

    $scope.clear = function () {
       
        $scope.ErrorMessage = null;
    }

    $scope.doLogin = function () {
        $scope.ErrorMessage = "";
        if ($scope.user_name == undefined || $scope.user_name == "") {
            //alert('User Name is required');
            $scope.ErrorMessage = "User Name is required";
            return false;
        }
        if ($scope.password == undefined || $scope.password== "") {
            //alert('Password is required');
            $scope.ErrorMessage = "Password is required";
            return false;
        }
        $scope.loginButtonText = "Please Wait...";
    var model = { user_name: $scope.user_name , password : $scope.password};
    var req = {
        method: "POST",
        url: "/Account/RESTLogin",
        headers: {
            'Content-Type': 'application/json',
            'dataType': 'json'
        },
        data: JSON.stringify(model)
    };

    $http(req).then(function (response) {

        $scope.loginButtonText = "Login";

        console.log(response.data);
        if (response.data.Success) {
             window.location.reload();
             
        }
        else {
            $scope.ErrorMessage = response.data.Message;
        }

    }, function (response) {
        console.log(response);
        $scope.loginButtonText = "Login";
    });
    }

    $scope.forgotPassword = function () {
        $scope.ErrorMessage = "";
        if ($scope.user_name == undefined || $scope.user_name == "") {
            $scope.ErrorMessage='User Name is required';
            return false;
        }
        if ($scope.email == undefined || $scope.email == "") {
            $scope.ErrorMessage='Email is required';
            return false;
        }
        $scope.forgotPasswordText = "Please Wait...";
        var model = { user_name: $scope.user_name, email: $scope.email };
        var req = {
            method: "POST",
            url: "/Account/RESTForgotPassword",
            headers: {
                'Content-Type': 'application/json',
                'dataType': 'json'
            },
            data: JSON.stringify(model)
        };

        $http(req).then(function (response) {
            
            $scope.forgotPasswordText = "Submit";

            console.log(response.data);
            if (response.data.Success) {
                console.log(response.data.Message);

            }
            else {
                $scope.ErrorMessage = response.data.Message;
            }

        }, function (response) {
            console.log(response);
            $scope.forgotPasswordText = "Submit";
        });
    }
});

app.controller('ProductController', function ($scope, $http) {
    
    $scope.cartList = [];
    var count = 0;
    $scope.getAllCart = function () {
        var req = {
            method: "GET",
            url: "/Product/GetCartList",
            headers: {
                'Content-Type': 'application/json',
                'dataType': 'json'
            } 
        };

        $http(req).then(function (response) {
            
            console.log(response.data);
            $scope.cartList = response.data;

        }, function (response) {
            console.log(response);

        });
    }
    $scope.getAllCart();
    $scope.addToCart = function () {
       
        var ItemNo = document.getElementById('sku').value;
        var Description = document.getElementById('description').value;
        var Price = document.getElementById('price').value;
        var Size = document.getElementById('size').value;
        var Title = document.getElementById('title').value;
        
        var id = document.getElementById('id').value;
       count++;
         
        var found = false;
        for (var i = 0; i < $scope.cartList.length; i++) {
            if ($scope.cartList[i].ID == id) {
                
               // $scope.cartList[i].Quantity = parseInt($scope.cartList[i].Quantity)+1;
                found = true;
            }   
        }
        var item = { "Serial": count, "ID": id, "Title": Title, "Description": Description, "Size": Size, "Price": Price, "Quantity": 1 };
        console.log(item);
        if (!found)
        $scope.cartList.push(item);

        var req = {
            method: "POST",
            url: "/Product/AddToCart",
            headers: {
                'Content-Type': 'application/json',
                'dataType': 'json'
            },
            data: JSON.stringify($scope.cartList)
        };

        $http(req).then(function (response) {
 
            console.log(response.data);
           

        }, function (response) {
            console.log(response);
           
        });
          
    }

    $scope.updateCart = function (id, quantity) {
        console.log('calling...');
        console.log(quantity);
        for (var i = 0; i < $scope.cartList.length; i++) {
            if ($scope.cartList[i].ID == id) {
                $scope.cartList[i].Quantity = quantity;
                console.log($scope.cartList);
                var req = {
                    method: "POST",
                    url: "/Product/AddToCart",
                    headers: {
                        'Content-Type': 'application/json',
                        'dataType': 'json'
                    },
                    data: JSON.stringify($scope.cartList)
                };

                $http(req).then(function (response) {

                    console.log(response.data);
                   

                }, function (response) {
                    console.log(response);

                });

            }
        }
    }

    $scope.removeCart = function (id) {
       
        for (var i = 0; i < $scope.cartList.length; i++) {
            if ($scope.cartList[i].ID == id) {
                var index = $scope.cartList.indexOf(i);
                $scope.cartList.splice(index, 1);
                var req = {
                    method: "POST",
                    url: "/Product/RemoveCart/"+id,
                    headers: {
                        'Content-Type': 'application/json',
                        'dataType': 'json'
                    },
                    data: JSON.stringify($scope.cartList)
                };

                $http(req).then(function (response) {

                    console.log(response.data);
                    //alert('Cart Removed');

                }, function (response) {
                    console.log(response);

                });

            }
        }
    }
    $scope.checkOutButtonText = "Check Out";
    $scope.checkOut = function () {
        if ($scope.cartList.length <= 0) {

            return;
        }
        $scope.CheckOutMessage = "";
        $scope.checkOutButtonText = "Processing...";
        var req = {
            method: "POST",
            url: "/Product/CheckOut",
            headers: {
                'Content-Type': 'application/json',
                'dataType': 'json'
            }
            
        };

        $http(req).then(function (response) {

            console.log(response.data);
            $scope.CheckOutMessage = response.data.Message;
            $scope.cartList = {};
            $scope.checkOutButtonText = "Check Out";

        }, function (response) {
            console.log(response);
            $scope.checkOutButtonText = "Check Out";

        });
    }
});

app.controller('CustomerController', function ($scope, $http) {
    $scope.profileButtonText = "Update";
    
    $scope.updateProfile = function () {
        $scope.ErrorMessage = "";
        if ($scope.user_name == undefined || $scope.user_name == "") {
            $scope.ErrorMessage='User Name is required';
            return false;
        }
        if ($scope.password == undefined || $scope.password == "") {
            $scope.ErrorMessage='Password is required';
            return false;
        }
        $scope.loginButtonText = "Please Wait...";
        var model = { user_name: $scope.user_name, password: $scope.password };
        var req = {
            method: "POST",
            url: "/Account/RESTLogin",
            headers: {
                'Content-Type': 'application/json',
                'dataType': 'json'
            },
            data: JSON.stringify(model)
        };

        $http(req).then(function (response) {

            $scope.loginButtonText = "Login";

            console.log(response.data);
            if (response.data.Success) {
                window.location.reload();

            }
            else {
                $scope.ErrorMessage = response.data.Message;
            }

        }, function (response) {
            console.log(response);
            $scope.loginButtonText = "Login";
        });
    }

    $scope.GetOrderList = function () {
        
        var req = {
            method: "GET",
            url: "/Account/MyOrder",
            headers: {
                'Content-Type': 'application/json',
                'dataType': 'json'
            }
        };

        $http(req).then(function (response) {

            console.log(response.data);
            $scope.orderList = response.data;

        }, function (response) {
            console.log(response);

        });
    }

    $scope.GetOrderList();
});


 