 var button_click = function() {
        var ModalID = this.getAttribute("rel");
        document.getElementById(ModalID).style.display = 'block';
    };

    var close_click = function() {
        var ModalID = this.getAttribute("rel");
        document.getElementById(ModalID).style.display = 'none';
    };


    // Get the button that opens the modal
    var btn = document.getElementsByClassName('MyBtn');

    // Get the <span> element that closes the modal
    var close = document.getElementsByClassName('close') ;

    // Assign the events to the buttons (open & close)
    for(iCount = 0; iCount < btn.length; iCount++) {
        btn[iCount].addEventListener('click', button_click, false) ;
        close[iCount].addEventListener('click', close_click, false) ;
    }