Element.prototype.remove = function () {
    this.parentElement.removeChild(this);
}

NodeList.prototype.remove = HTMLCollection.prototype.remove = function () {
    for (var i = this.length - 1; i >= 0; i--) {
        if (this[i] && this[i].parentElement) {
            this[i].parentElement.removeChild(this[i]);
        }
    }
}

var ShowImage = function (file, id) {
    var reader = new FileReader
    var image = new Image

    reader.readAsDataURL(file)

    reader.onload = function (_file) {
        image.src = _file.target.result

        image.onload = function () {
            var divImgPrev = document.createElement("div")
            divImgPrev.classList = "thumbnail form-group col-md-4"
            divImgPrev.id = id

            var img = document.createElement("img")
            img.className = "img-responsive"
            img.src = _file.target.result
            img.style.width = "100px"
            img.style.height = "100px"

            var divCaption = document.createElement("div")
            divCaption.className = "caption"

            var iErase = document.createElement("i")
            iErase.classList = "glyphicon glyphicon-trash"

            var erase = document.createElement("a")
            erase.href = "#"
            erase.onclick = function () { ClearPreview(id); };

            erase.appendChild(iErase)

            divCaption.appendChild(erase)

            divImgPrev.appendChild(img)
            divImgPrev.appendChild(divCaption)

            document.getElementById("imagesPreview").appendChild(divImgPrev)
        }
    }
}

var ClearPreview = function (id) {
    var files = $(".imageUpload").val()


    document.getElementById(id).remove()
}