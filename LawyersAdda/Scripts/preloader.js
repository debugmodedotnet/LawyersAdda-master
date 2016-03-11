(function($) {
    "use strict";
    //Hide Loading Box (Preloader)
    function handlePreloader() {
        if($('.preloader').length){
            $('.preloader').delay(500).fadeOut(500);
        }
    }
    $(window).on('load', function () {
        handlePreloader();
    });
})(window.jQuery);