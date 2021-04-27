let base64imageValue = "";
let contentType = "";
let listRestaurants = [];

$(function () {
  //get list restaurants;
  getList();
});
function getList() {
  data = {};
  fetch("http://localhost:4000/api/restaurant/list", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    .then((response) => response.json())
    .then((res) => {
      listRestaurants = res.list ?? [];

      let html = "";

      listRestaurants.forEach((e, i) => {
        html += `
                <tr>
              <td>${++i}</td>
              <td>${e.restaurantName}</td>
              <td>${e.restaurantAddress}</td>
              <td>${e.restaurantEmail}</td>
              <td>${e.restaurantPhone}</td>
              <td>
                <div class="btn btn-sm btn-warning" data-toggle="modal" data-target="#modalRestaurant" onclick="editRecord('${e.restaurantId
          }')">Edit</div>
                <div class="btn btn-sm btn-danger" onclick="deleteRecord('${e.restaurantId
          }')">Delete</div>
              </td>
            </tr>
                `;
      });
      $("#tableBody").html(html);
    })
    .catch((error) => {
      console.error("Error:", error);
    });
}
$("#openModalBtn").click(() => {
  $("#id").val(0);
  $("#branch-form").trigger("reset");
  $("#oldImg").hide()
})

function editRecord(restaurantId) {
  let restaurant = listRestaurants.find((e) => e.restaurantId == restaurantId);

  if (!restaurant) return;

  $("form #id").val(restaurantId);

  // const formData = new FormData(document.getElementById('branch-form'));
  $("form #restaurantName").val(restaurant.restaurantName);
  $("#restaurantAddress").val(restaurant.restaurantAddress);
  $("#restaurantEmail").val(restaurant.restaurantEmail);
  $("#restaurantPhone").val(restaurant.restaurantPhone);

  let html = "";
  html += `<img src=${restaurant.imageSrc} alt=${restaurant.imageName}">`;

  $("#oldImg").append(html);

  $("#createBtn").html("Save");
}

function deleteRecord(restaurantId) {
  data = {
    'restaurantId': Number(restaurantId)
  };

  console.log(data)

  swal({
    title: "Are you sure?",
    text: "This restaurant will not be recovered!",
    icon: "warning",
    buttons: true,
    dangerMode: true,
  }).then((willDelete) => {
    if (willDelete) {
      swal("This restaurant has been deleted!", {
        icon: "success",
        buttons: false,
        timer: 1500,
      });
      setTimeout(() => {
        fetch("http://localhost:4000/api/restaurant/delete", {
          method: "POST", // or 'PUT'
          headers: {
            "Content-Type": "application/json",
          },
          body: JSON.stringify(data),
        })
          .then((response) => response.json())
          .then((res) => {
            console.log("Success:", res);
            let response = { ...res };
            if (response.statusCode === 200) {
              // thoong bao thanh cong
              getList();
            } else {
              // thong bao loi
            }
          })
          .catch((error) => {
            console.error("Error:", error);
          });
      }, 1000);
      return;
    }
  });

  // if (confirm("Ban chac chan muon xoa hay khong?")){
  //   fetch("http://localhost:4000/api/restaurant/delete", {
  //     method: "POST", // or 'PUT'
  //     headers: {
  //       "Content-Type": "application/json",
  //     },
  //     body: JSON.stringify(data),
  //   })
  //     .then((response) => response.json())
  //     .then((res) => {
  //       console.log("Success:", res);
  //       let response = { ...res };
  //       if (response.statusCode === 200) {
  //         // thoong bao thanh cong
  //         swal("The restaurant been deleted!", {
  //           icon: "success",
  //           buttons: false,
  //           timer: 1500,
  //         });
  //         getList();
  //       } else {
  //         // thong bao loi
  //       }
  //     })
  //     .catch((error) => {
  //       console.error("Error:", error);
  //     });
  // }
}

async function createRestaurant() {
  // validate

  // thuoc tinh mapping voi thuoc tinh cua api
  let restaurantModel = {
    restaurantName: "",
    restaurantAddress: "",
    restaurantEmail: "",
    restaurantPhone: "",
    longitude: 0,
    latitude: 0,
    base64Value: base64imageValue,
    imageName: "",
    contentType: contentType,
  };

  let fileInput = document.getElementById("restaurantImage");
  if (fileInput) {
    if (fileInput.files.length != 0) {
      restaurantModel.imageName = fileInput.files[0].name;
    }
  }
  const formData = new FormData(document.getElementById("branch-form"));
  let restaurant_ID = 0;
  for (var pair of formData.entries()) {
    if (pair[0] == "restaurantName") restaurantModel.restaurantName = pair[1];
    if (pair[0] == "restaurantAddress")
      restaurantModel.restaurantAddress = pair[1];
    if (pair[0] == "restaurantEmail") restaurantModel.restaurantEmail = pair[1];
    if (pair[0] == "restaurantPhone") restaurantModel.restaurantPhone = pair[1];

    if (pair[0] == "id") restaurant_ID = pair[1];
  }

  console.log('restaurant_ID:', restaurant_ID)


  if (restaurant_ID == 0) {
    data = { ...restaurantModel };
    $("#createBtn").html("Save");

    fetch("http://localhost:4000/api/restaurant/create", {
      method: "POST", // or 'PUT'
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((response) => response.json())
      .then((res) => {
        console.log("Success:", res);
        let response = { ...res };
        if (response.statusCode === 200) {
          swal("The restaurant has been ADDED!", {
            icon: "success",
            buttons: false,
            timer: 1500,
          });
          getList();
        } else {
          // thong bao loi
        }
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  } else {
    restaurantModel.restaurantId = Number(restaurant_ID);
    data = { ...restaurantModel };
    console.log(data)
    fetch("http://localhost:4000/api/restaurant/update", {
      method: "POST", // or 'PUT'
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(data),
    })
      .then((response) => response.json())
      .then((res) => {
        console.log("Success:", res);
        let response = { ...res };
        if (response.statusCode === 200) {
          swal("The restaurant has been SAVED!", {
            icon: "success",
            buttons: false,
            timer: 1500,
          });
          setTimeout(()=> {
            location.reload();
          }, 1300)
        } else {
        }
      })
      .catch((error) => {
        console.error("Error:", error);
      });
  }
}
var input = document.querySelector("input[type=file]");
input.onchange = function () {
  var file = input.files[0],
    reader = new FileReader();
  reader.onloadend = function () {
    var b64 = reader.result.replace(/^data:.+;base64,/, "");
    base64imageValue = b64;

    console.log("b64:", b64);

    // let b64ContentType = reader.result.match(/^data:.+;base64,/);
    // console.log('b64ContentType', b64ContentType)

    // contentType = b64ContentType[0]['0'];
    // console.log('contentType: ', contentType)
    let listString = reader.result.split(",");

    contentType = listString[0];
  };

  reader.readAsDataURL(file);
};

$("#restaurantImage").change(() => {
  let path = $("#restaurantImage").val();
  let fileName = path.split("\\").pop();
  $(".custom-file label").text(fileName);
});

