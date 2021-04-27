// let base64imageValue = "";
let contentType = "";
let listAccounts = [];

$(function () {
  //get list customer;
  getList()
});

function getList() {
  data = {};
  fetch("http://localhost:4000/api/customer/list", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(data),
  })
    .then((response) => response.json())
    .then((res) => {
      console.log(res)
      listAccounts = res.listCustomer ?? [];

      console.log(listAccounts);

      let html = "";

      listAccounts.forEach((e, i) => {
        html += `
                <tr>
              <td>${++i}</td>
              <td>${e.customerName}</td>
              <td>${e.customerEmailId}</td>
              <td>${e.customerContactPhone}</td>
              <td>${e.customerAddress}</td>
              <td>
                <div class="btn btn-sm btn-warning" data-toggle="modal" data-target="#modalStaff" onclick="editRecord('${e.customerId
          }')">Edit</div>
                <div class="btn btn-sm btn-danger" onclick="deleteRecord('${e.customerId
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
  $("#sltRestaurant").hide();
  $("#checkboxIsStaff").removeAttr("checked")
})

function editRecord(accountId) {
  let account = listAccounts.find((e) => e.customerId == accountId);
console.log(account);
  if (!account) return;

  $("form #id").val(accountId);

  $("#accountName").val(account.customerName);
  
  $("#accountEmail").val(account.customerEmailId);

  $("#accountAddress").val(account.customerAddress);

  $("#accountPhone").val(account.customerContactPhone);

  if (account.isStaff == true || account.isStaff == "true"|| account.isStaff == 1) {
    $("#checkboxIsStaff").attr("checked", "checked")
    bindRestaurants()
    $("#sltRestaurant").show();
  } else { 
    $("#checkboxIsStaff").removeAttr("checked")
    $("#sltRestaurant").hide();
  }
  
  $("#userName").val(account.username);

  $("#userPassword").val(account.password);

  $("#createBtn").html("Save");
}

function deleteRecord(accountId) {
  data = {
    'customerId': Number(accountId)
  };

  console.log("delete")
  console.log(data)

  swal({
    title: "Are you sure?",
    text: "This customer will not be recovered!",
    icon: "warning",
    buttons: true,
    dangerMode: true,
  }).then((willDelete) => {

    if (willDelete) {

      swal("This customer has been deleted!", {
        icon: "success",
        buttons: false,
        timer: 1300,
      });

      setTimeout(() => {
        fetch("http://localhost:4000/api/customer/delete", {
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
              getList();
            } else {
              console.log("loi")
            }
          })
          .catch((error) => {
            console.error("Error:", error);
          });
      }, 1000);
    }
  });
}

async function createAccount() {
  // validate

  // thuoc tinh mapping voi thuoc tinh cua api
  let staffModel = {
    "customerName": "string",
    "customerEmailId": "string",
    "customerContactPhone": "string",
    "username": "string",
    "password": "string",
    "customerAddress": "string",
    "isStaff": true,
    "restaurantId": 0,
  };

  const formData = new FormData(document.getElementById("branch-form"));

  let staff_ID = 0;

  for (var pair of formData.entries()) {

    console.log(pair[1])

    if (pair[0] == "accountName") staffModel.customerName = pair[1];

    if (pair[0] == "accountEmail")
      staffModel.customerEmailId = pair[1];

    if (pair[0] == "accountAddress") staffModel.customerAddress = pair[1];

    if (pair[0] == "accountPhone") staffModel.customerContactPhone = pair[1];

    if (pair[0] == "userName") staffModel.username = pair[1];

    if (pair[0] == "userPassword") staffModel.password = pair[1];

    if ($("#checkboxIsStaff").prop('checked') === true) {
      if (pair[0] == 'selectRestaurant')
      {
        staffModel.restaurantId =  pair[1] ? Number(pair[1]) : 0 ;
      }
    } else {
      staffModel.restaurantId = 0
    }

    if (pair[0] == "id") staff_ID = pair[1];
  }

  console.log('staff_ID:', staff_ID)

  if (staff_ID == 0) {
    // alert("api/customer/create")
    data = {customer:{...staffModel} };

    $("#createBtn").html("Save");

    fetch("http://localhost:4000/api/customer/create", {
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
          swal("The account has been ADDED!", {
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
    staffModel.customerId = Number(staff_ID);
    data = {customer:{...staffModel} };
    console.log(data)
    fetch("http://localhost:4000/api/customer/update", {
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
          swal("The account has been SAVED!", {
            icon: "success",
            buttons: false,
            timer: 1500,
          });
          setTimeout(() => {
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

let bindRestaurants = () => {
  /* restaurant dropdown */
  fetch("http://localhost:4000/api/restaurant/list", {
    method: 'POST', // or 'PUT'
    headers: {
      'Content-Type': 'application/json',
    },
    data: null,
  })
    .then(response => response.json())
    .then(res => {
      let response = { ...res };
      if (response.statusCode === 200) {
        var html = '';
        for (let i = 0; i < response.list.length; i++) {
          html += '<option value="' + response.list[i].restaurantId + '">' + response.list[i].restaurantName + '</option>';
        }
        $('#sltRestaurant').html(html);
      } else {
        alert('Fail');
      }
    })
    .catch((error) => {
      console.error('Error:', error);
    });
}


$(() => {
  showSelect()
})
$('#sltRestaurant').hide();
function showSelect () {
$('#checkboxIsStaff').change(() => {
  if ($("#checkboxIsStaff").prop('checked') === true) {
    bindRestaurants()
    $("#sltRestaurant").show();
  } else {
    $('#sltRestaurant').hide();
  }
})
}