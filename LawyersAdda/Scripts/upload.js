+ function($) {
    'use strict';

    // UPLOAD CLASS DEFINITION
    // ======================

    var dropZone = document.getElementById('drop-zone');
    var uploadForm = document.getElementById('js-upload-form');
    var uploadFormFiles = document.getElementById('js-upload-files');
    var uploadedImage = document.getElementById("uploadedImage");

    var filesToUpload;

    var startUpload = function(files) {
        alert("call api to upload");
        var data = new FormData();
        for (var x = 0; x < files.length; x++) {
            data.append("file" + x, files[x]);
        }
        console.log(data);
        var progress = document.getElementById("progress");
        progress.style.display = "inline";
        $.ajax({
            type: "POST",
            url: 'http://localhost:63025/Image/AddImage',
            contentType: false,
            processData: false,
            data: data,
            success: function(result) {
                console.log(result);
                uploadedImage.style.display = "block";
                //uploadedImage.innerHTML = uploadedImage.innerHTML + "<a href='" + result + "' class='list-group-item list-group-item-success'><span class='badge alert-success pull-right'>Success</span>" + files[0].name.toString() + "</a>";
                uploadedImage.innerHTML = uploadedImage.innerHTML + "<div class='col-md-4'><img class='img-thumbnail img-responsive' src='" + result + "' /></div>";
                uploadedImage.setAttribute("href", result);
                progress.style.display = "none";
            },
            error: function (xhr, status, p3, p4){
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
            }
        });

    }

    uploadFormFiles.addEventListener('change', function (e) {
        filesToUpload = e.target.files;
    })

    uploadForm.addEventListener('submit', function (e) {
        var uploadFiles = filesToUpload;
        e.preventDefault();
        startUpload(uploadFiles);
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