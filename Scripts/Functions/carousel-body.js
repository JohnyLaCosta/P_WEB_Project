function getImages(id) {
    const carousel = document.getElementById("carousel-content")

    while (carousel.firstChild) {
        carousel.removeChild(carousel.firstChild)
    }

    $.ajax({
        url: "/api/images",
        type: "GET",
        contentType: "application/json",
        dataType: "json",
        data: {
            vehicleId: id
        },
        success: function (res) {
            createCorousel(res)
        }
    })
}


var createCorousel = function (array) {
    var firstTime = true;
    var i = 0;

    while (1) {
        try {
            var img = document.createElement("img")
            img.src = array[i].Name.substr(1)
            img.style.width = "100%"
            img.style.height = "300px"
            i++

            var div = document.createElement("div")

            if (firstTime) {
                div.classList = "item active"
                firstTime = false
            } else {
                div.className = "item"
            }

            div.appendChild(img)

            document.getElementById("carousel-content").appendChild(div)
        } catch (e) {
            console.log(e)
            break
        }

    }
}