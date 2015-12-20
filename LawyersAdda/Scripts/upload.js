+ function($) {
    'use strict';

    // UPLOAD CLASS DEFINITION
    // ======================

    var dropZone = document.getElementById('drop-zone');
    var uploadForm = document.getElementById('js-upload-form');

    var startUpload = function(files) {
        alert("call api to upload");
        console.log(files);
        $.ajax({
            cache: false,
            type: "POST",
            url: "http://localhost:63025/Image/AddImage",
            success: function (data) {                      
                alert(data);
                //statesProgress.hide();
        },
        error: function (xhr, ajaxOptions, thrownError) {
            alert('Failed to retrieve states.');
            statesProgress.hide();
        }
    });
    }

    uploadForm.addEventListener('submit', function(e) {
        var uploadFiles = document.getElementById('js-upload-files').files;
        e.preventDefault()

        startUpload(uploadFiles)
    })

    dropZone.ondrop = function(e) {
        e.preventDefault();
        this.className = 'upload-drop-zone';

        startUpload(e.dataTransfer.files)
    }

    dropZone.ondragover = function() {
        this.className = 'upload-drop-zone drop';
        return false;
    }

    dropZone.ondragleave = function() {
        this.className = 'upload-drop-zone';
        return false;
    }

}(jQuery);