let contentType = "";
let listCategories = [];


$(function () {
    //get list restaurants;
    getList();
  });

function getList () {
    //get list restaurants;
    data = {};
    fetch("http://localhost:4000/api/category/list", {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
        },
        body: JSON.stringify(data),
    })
        .then((response) => response.json())
        .then((res) => {
            listCategories = res.list ?? [];

            console.log(listCategories);

            let html = "";

            listCategories.forEach((e, i) => {
                html += `
                <tr>
              <td>${++i}</td>
              <td>${e.categoryName}</td>
              <td>
                <div class="btn btn-sm btn-warning" data-toggle="modal" data-target="#modalAddRestaurant" onclick="editRecord('${
                    e.categoryId
                }')">Edit</div>
                <div class="btn btn-sm btn-danger" onclick="deleteRecord('${
                    e.categoryId
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
};

$("#openModalBtn").click(() => {
    $("#id").val(0);
    $("#branch-form").trigger("reset");
    $("#oldImg").hide()
})

function editRecord(categoryId) {
    let restaurant = listCategories.find((e) => e.categoryId == categoryId);

    if (!restaurant) return;

    $("form #id").val(categoryId);

    $(" #categoryName").val(restaurant.categoryName);

    $("#createBtn").html("Save");
}

function deleteRecord(categoryId) {
    data = {
        'categoryId': Number(categoryId)
    };

    console.log(data)

    swal({
        title: "Are you sure?",
        text: "This category will not be recovered!",
        icon: "warning",
        buttons: true,
        dangerMode: true,
      }).then((willDelete) => {
        if (willDelete) {
          swal("This category has been deleted!", {
            icon: "success",
            buttons: false,
            timer: 1500,
          });
          setTimeout(() => {
            fetch("http://localhost:4000/api/category/delete", {
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
                    getList()
                } else {
                    // thong bao loi
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });

          }, 1000);
        }
      });
    }

async function createRestaurant() {
    let categoryModal = {
        categoryName: "",
    };

    const formData = new FormData(document.getElementById("branch-form"));
    let category_ID = 0;
    for (var pair of formData.entries()) {
        if (pair[0] == "categoryName") categoryModal.categoryName = pair[1];

        if (pair[0] == "id") category_ID = pair[1];
    }

    console.log('category_ID:',category_ID)

    if (category_ID == 0) {
        data = { ...categoryModal };
        $("#createBtn").html("Save");

        fetch("http://localhost:4000/api/category/create", {
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
                    swal("The category has been ADDED!", {
                        icon: "success",
                        buttons: false,
                        timer: 1500,
                      });
                      getList();
                } else {
                    location.reload();
                }
            })
            .catch((error) => {
                console.error("Error:", error);
            });
    } else {
        categoryModal.categoryId = Number(category_ID);
        data = { ...categoryModal };
        console.log(data)
        fetch("http://localhost:4000/api/category/update", {
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
