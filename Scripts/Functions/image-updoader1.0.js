var counter

$(document).ready(function () {
    counter = 1
})

$(".imgAdd").click(function () {
    if (counter < 3) {
        $(this).closest(".row").find('.before').before('<div class="col-sm-3 col-md-3 col-xs-3 col-lg-3 imgUp"><div class="imagePreview"></div><label class="btn btn-primary" style="display:block; border-radius: 0px; box-shadow:0px 4px 6px 2px rgba(0,0,0,0.2); margin-top:-5px;">Upload<input type="file" class="uploadFile img" name="imageFiles" value="Upload Photo" accept="image/*" style="width:0px;height:0px;overflow:hidden;"></label><i class="fa fa-times del"></i></div>');
        counter++
        if (counter == 3) {
            $(".before").hide()
        }
    }
});

$(document).on("click", "i.del", function () {
    $(this).parent().remove();
    if (counter == 3) {
        $(".before").show()
    }
    if (counter > 0)
        counter--;
});

$(function () {
    $(document).on("change", ".uploadFile", function () {
        var uploadFile = $(this);
        var files = !!this.files ? this.files : [];
        if (!files.length || !window.FileReader) return; // no file selected, or no FileReader support

        if (/^image/.test(files[0].type)) { // only image file
            var reader = new FileReader(); // instance of the FileReader
            reader.readAsDataURL(files[0]); // read the local file

            reader.onloadend = function () { // set image data as background of div
                //alert(uploadFile.closest(".upimage").find('.imagePreview').length);
                uploadFile.closest(".imgUp").find('.imagePreview').css("background-image", "url(" + this.result + ")");
            }
        }

    });
});