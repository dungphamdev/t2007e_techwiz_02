$('.Slick').slick({
    dots: true,
    infinite: false,
    speed: 300,
    slidesToShow: 5,
    slidesToScroll: 5,
    responsive: [
        {
            breakpoint: 1024,
            settings: {
                slidesToShow: 4,
                slidesToScroll: 4,
                infinite: true,
                dots: true
            }
        },
        {
            breakpoint: 600,
            settings: {
                slidesToShow: 3,
                slidesToScroll: 3,
            }
        },
        {
            breakpoint: 480,
            settings: {
                slidesToShow: 2,
                slidesToScroll: 2,
            }
        }
        // You can unslick at a given breakpoint now by adding:
        // settings: "unslick"
        // instead of a settings object
    ]
});

$(function () {
    //get list restaurants;
    data = {};
    fetch('http://localhost:4000/api/restaurant/list', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json',
        },
        body: JSON.stringify(data),
    })
        .then(response => response.json())
        .then(res => {
            let listRestaurants = res.list ?? [];
            //generate DOM
            let restaurants='';
            console.log(listRestaurants);
            let options = {
                data:listRestaurants,
                getValue: "restaurantName",
                list: {
                    match: {
                        enabled: true
                    }
                }
            };
            $("#sh").easyAutocomplete(options)
            let autoCompleted=[];
            listRestaurants.forEach(e => {
                autoCompleted.push(e.restaurantName)
                restaurants += `<div class="col-md-3 col-sm-6 mt-5">`
                restaurants += `<img src=${e.imageSrc} alt=${e.imageName}" width="220px" height="auto" alt=" ">`
                restaurants += `<br/><br/>`
                restaurants += `<strong>${e.restaurantName}</strong>`
                restaurants += `<br/><br/>`
                restaurants += `<span><i class="fas fa-caret-square-down" style="font-size:13px;"></i> Menu: </span> `
                restaurants += `<br/> `
                restaurants += ` <span><i class="fas fa-map-marked-alt" style="font-size:13px;" ></i> Address: </span> ${e.restaurantAddress}`
                restaurants += `</div>`
            });

            $("#restaurant").append(restaurants)

        })
        .catch((error) => {
            console.error('Error:', error);
        });
});