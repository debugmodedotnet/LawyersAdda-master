+ function($) {
    'use strict';

    // UPLOAD CLASS DEFINITION
    // ======================

    //var dropZone = document.getElementById('drop-zone');
    //var uploadForm = document.getElementById('js-upload-form');
    var uploadForm = document.getElementById('js-upload-submit');
    var uploadFormFiles = document.getElementById('js-upload-files');
    var uploadedImage = document.getElementById("uploadedImage");
    var documentID=document.getElementById("DocumentID").value;
    var ext = "";

    var filesToUpload;
    var UploadedFlag = false;
    var IncreaseProgress = function () {
        var valueOfProgress = Number($("#progress").attr("value"));
        if (valueOfProgress != 100) {
            $("#progress").attr("value", valueOfProgress + 1);
            setTimeout(IncreaseProgress, 110);
        }
        else {
            if (UploadedFlag == false) {
                $("#ProgressHeader").html("Uploaded Successfully...");
                UploadedFlag = true;
                setTimeout(IncreaseProgress, 2500);
            }
            else {
                $("#uploadingData").hide();
                UploadedFlag = false;
                $("#progress").attr("value", 0);
                $("#ProgressHeader").html("Please Wait File is Uploading...");
            }
        }
    }
    var startUpload = function(files) {
        //alert("call api to upload");
        $("#uploadingData").show();
        $("#progress").attr("value",0);
        setTimeout(IncreaseProgress, 300);
        var data = new FormData();
        for (var x = 0; x < files.length; x++) {
            data.append("file" + x, files[x]);
        }
        console.log(data);
        //var progress = document.getElementById("progress");
        //progress.style.display = "inline";
        $.ajax({
            type: "POST",
            url: '/File/AddFile?id=' + documentID,
            contentType: false,
            processData: false,
            data: data,
            success: function(result) {
                console.log(result);
                if (ext == "pdf") {
                    //Create thumbnil for PDF
                }
                else {
                    uploadedImage.style.display = "block";
                    uploadedImage.innerHTML = uploadedImage.innerHTML + "<div class='col-md-4'><img class='img-thumbnail img-responsive' src='" + result + "' /></div>";
                    uploadedImage.setAttribute("href", result);
                }

                $("#progress").attr("value", 100);
                $("#ProgressHeader").html("Uploaded Successfully...");
            },
            error: function (xhr, status, p3, p4){
                var err = "Error " + " " + status + " " + p3 + " " + p4;
                if (xhr.responseText && xhr.responseText[0] == "{")
                    err = JSON.parse(xhr.responseText).Message;
                console.log(err);
                $("#progress").attr("value", 100);
                $("#ProgressHeader").html("Error in Uploading...");
            }
        });
    }

    uploadFormFiles.addEventListener('change', function (e) {
        var file = $(this).val();
        ext = file.split('.').pop();
        filesToUpload = e.target.files;
    })

    uploadForm.addEventListener('click', function (e) {
        var uploadFiles = filesToUpload;
        e.preventDefault();
        startUpload(uploadFiles);
    })

    //dropZone.ondrop = function(e) {
    //    e.preventDefault();
    //    this.className = 'upload-drop-zone';

    //    startUpload(e.dataTransfer.files)
    //}

    //dropZone.ondragover = function() {
    //    this.className = 'upload-drop-zone drop';
    //    return false;
    //}

    //dropZone.ondragleave = function() {
    //    this.className = 'upload-drop-zone';
    //    return false;
    //}

}(jQuery);