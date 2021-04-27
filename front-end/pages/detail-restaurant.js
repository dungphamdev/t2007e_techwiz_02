let listItems = [];

//const { data } = require("jquery");
var item = [];
function myFunction() {
  var dots = document.getElementById("dots");
  var moreText = document.getElementById("more");
  var btnText = document.getElementById("myBtn");

  if (dots.style.display === "none") {
    dots.style.display = "inline";
    btnText.innerHTML = "Read more";
    moreText.style.display = "none";
  } else {
    dots.style.display = "none";
    btnText.innerHTML = "Read less";
    moreText.style.display = "inline";
  }
}


// $(function () {
//   //get list items;
//   data = {};
//   fetch('http://localhost:4000/api/item/list', {
//       method: 'POST',
//       headers: {
//           'Content-Type': 'application/json',
//       },
//       body: JSON.stringify(data),
//   })
//       .then(response => response.json())
//       .then(res => {
//           let listItem = res.list ?? [];
//           //generate DOM
//           let items='';
//           console.log(listItem);
//           let options = {
//               data:listItem,
//               getValue: "restaurantName",
//               list: {
//                   match: {
//                       enabled: true
//                   }
//               }
//           };  

//       })
//       .catch((error) => {
//           console.error('Error:', error);
//       });
// });

$(function () {
  if(localStorage.getItem('currentRestaurant') == null)
  {
    return;
  }
  let restaurant = JSON.parse(localStorage.getItem('currentRestaurant'));
  let currentrestaurantId = restaurant.currentRestaurantId;
  model = {
    'RestaurantId' : Number(currentrestaurantId)
  };
  fetch('http://localhost:4000/api/restaurant/getRestaurantById', {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json',
      },
      body: JSON.stringify(model),
  })
  .then(response => response.json())
  .then(res => {
    var result = JSON.stringify(res);
    var detailRestaurant = JSON.parse(result);
    
    var listItemofcurrentRestaurant = detailRestaurant.restaurantDetail.listItem;
    let html = '';
    let html1 = `<h2 >${detailRestaurant.restaurantDetail.restaurantName}</h2>
    <p>&nbsp;</p>
    <p style="color: #a0a0a0" class="ml-2">
      ${detailRestaurant.restaurantDetail.restaurantAddress}
      <br/>
      Email: ${detailRestaurant.restaurantDetail.restaurantEmail} - Contact phone number: ${detailRestaurant.restaurantDetail.restaurantPhone}
    </p>
    <p class="ml-2" style="color: #a0a0a0">
      <span>Opening time: </span>Today 08:00-21:30
    </p>`
    console.log(detailRestaurant.restaurantDetail)
    //html2 += detailRestaurant.
    $('#restaurantDeltai').html(html1);
    for(var i=0; i<listItemofcurrentRestaurant.length;i++)
    {
      let iteminfo = {
        'id' : listItemofcurrentRestaurant[i].itemId,
        'image': listItemofcurrentRestaurant[i].imageSrc,
        'name' : listItemofcurrentRestaurant[i].itemName,
        'desc' : listItemofcurrentRestaurant[i].itemDescription,
        'price' : listItemofcurrentRestaurant[i].itemPrice
      };
      item.push(JSON.stringify(iteminfo));
      html += `<div class="col-md-4 col-sm-10">
      <div class="row">
        <div class="col-5">
          <div>
            <img width="120px" height="180px" src=${listItemofcurrentRestaurant[i].imageSrc} alt="${listItemofcurrentRestaurant[i].imageName}">
          </div>
        </div>
        <div class="col-7 text-left">
          <p>${listItemofcurrentRestaurant[i].itemName}</p>
          <span>${listItemofcurrentRestaurant[i].itemDescription}</span>
          <span><br>-----------------------------</span>
          <div class="row">
            <div class="col-6">
              <p style="font-weight: 700; font-size: 18px">$${listItemofcurrentRestaurant[i].itemPrice}</p>
            </div>
            <div class="col-6">
              <div class="mt-1 ml-5">
                <!-- <button data-toggle="modal" data-target="#order-1">Start Modal</button> -->
                <a href="" data-toggle="modal" data-target="#order-2" onclick="showModalOrder('${listItemofcurrentRestaurant[i].itemId}')">
                  <img width="30px" height="auto" src="../food-restaurant/plus1.png" alt="plusSign">
                </a>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
    <p>&nbsp;</p>
    <p>&nbsp;</p>
    `;
    }
    $('#mainproduct').html(html);
  })
  .catch((error) => {
      //alert('Too many item in Cart');
      console.error('Error:', error);
  });
})
var itemclone = {};
function showModalOrder(id)
{
  //let parsItem = JSON.parse(item);
  
  for(let i=0;i<item.length;i++)
  {
    let parsItem = JSON.parse(item[i]);
    if(Number(id) == Number(parsItem.id))
    {
      itemclone = parsItem;
    }
  }
 
  let html = `                
  <div class="col-md-3">
  <div class="productImg">
    <img
      class="img-fluid"
      src="${itemclone.image}"
      alt=""
    />
  </div>
</div>
<div class="col-md-9">
  <div class="row d-flex justify-content-start">
    <div class="col-md-6 text-left">
      <h5 class="text-dark font-weight-bold">
      ${itemclone.name}
      </h5>
    </div>
    <div class="col-md-6">
      <h5 id="priceInOrder" class="text-dark font-weight-bold">
        $${itemclone.price}
      </h5>
    </div>
    <div class="col-md-12 text-left small">
      <span> ${itemclone.desc}</span>
    </div>
  </div>
</div>`;
  $('#infoitem').html(html); 
}
let arrObj = [];
$(function () {
  loaditemfromCart();
})
function loaditemfromCart()
{
  let cartclone = JSON.parse(localStorage.getItem('currentCart'));
  let data = {
    "listItemId": [...cartclone]
  }
  if(cartclone.length > 0)
  {
    
    fetch('http://localhost:4000/api/item/getImageById', {
      method: 'POST',
      async: false,
      headers: {
          'Content-Type': 'application/json',
      },
      body: JSON.stringify(data),
    })
    .then(response => response.json())
    .then(res => {
      var result = JSON.stringify(res); 
      listItems = res.listItem;
      let html = '';
      let parseResult = JSON.parse(result).listItem;
      let money = 0;
     

      for(let i = 0; i<parseResult.length;i++)
      {
        let objecttest = {
          'itemId' : '',
          'itemPrice' : '',
          'itemQty' : 1,
        };
        objecttest.itemId = parseResult[i].itemId;
        objecttest.itemPrice = parseResult[i].itemPrice;

        arrObj.push(objecttest);
        money += Number(parseResult[i].itemPrice);
        html += `              
        <div class="row productInBasket py-3 border-bottom">
        <div class="col-md-3 col-4 pr-0">
          <button
            class="adjustAmountBtn-small decreaseBtn-2 p-0 bg-transparent text-success"
            type="button"
          >
            <h4 class="font-weight-bold">-</h4>
          </button>
          <input id="${parseResult[i].itemId}"
            type="number"
            class="quantityInBasket border-0 text-center"
            min="1"
            value="1"
          />
          <button
            class="adjustAmountBtn-small increaseBtn-2 p-0 bg-transparent text-success"
            type="button"
          >
            <h4 class="font-weight-bold">+</h4>
          </button>
        </div>
        <div class="col-md-9 col-8 p-0">
          <div class="row">
            <div class="col-3">
              <div class="productImgInBasket">
                <img
                  height="200px" width="auto"
                  class="img-fluid"
                  src="${parseResult[i].imageSrc}"
                  alt=""
                />
              </div>
            </div>
            <div class="col-9">
              <div class="font-weight-bold">${parseResult[i].itemName}</div>
              <small class="font-weight-bold">Prompt: </small
              ><small class="small">No onions, please.</small>
            </div>
          </div>
        </div>
        <button class="btn btn-danger" onclick="removeItemintheCart('${parseResult[i].itemId}')">x</button>
      </div>
      `;
      }
      $('#money').html('$'+money);
      // generateTotalText();
      
      $('#mainCart').html(html);
      
      $(".productInBasket").each(function(e){
        console.log(arrObj[e])
        $(this).find(".decreaseBtn-2").click(function(){
       
            let quantity = $(this).parent().find(".quantityInBasket").val();
            if (quantity > 1) {
                
                $(this).parent().find(".quantityInBasket").val(quantity - 1);
                arrObj[Number(e)].itemQty = Number(quantity) -1;
                console.log(arrObj)
                money -= arrObj[Number(e)].itemPrice;
                $('#money').html('$'+money);
            }
            let Money = Number($('#money').text().replace('$',''));
            // generateTotalText();
        });
        $(this).find(".increaseBtn-2").click(function(){
          let Money = Number($('#money').text().replace('$',''));
            let quantity = $(this).parent().find(".quantityInBasket").val();
            $(this).parent().find(".quantityInBasket").val(parseInt(quantity) + 1);
            arrObj[Number(e)].itemQty = Number(quantity) +1;
            money += arrObj[Number(e)].itemPrice;
            $('#money').html('$'+money);
            // generateTotalText();
        });
        
      });
      
    })
    .catch((error) => {
        //alert('Too many item in Cart');
        console.error('Error:', error);
    });
  }
  else
  {
    $('#mainCart').html('');
    $('#money').html('');
  }
}
function removeItemintheCart(itemId)
{
  let cartclone = JSON.parse(localStorage.getItem('currentCart'));
  
  for(var i=0;i<cartclone.length;i++)
  {
    if(Number(itemId) == Number(cartclone[i]))
    {
      cartclone.splice(i,1);
      break;
    }
  }
  console.log(cartclone)
  localStorage.removeItem('currentCart');
  localStorage.setItem('currentCart',JSON.stringify(cartclone));
  loaditemfromCart();
}

function addtoCart()
{
 
  if(localStorage.getItem('customer') == null)
  {
    alert('Please login!');
    return;
  }
  let cart = JSON.parse(localStorage.getItem('currentCart'));
  if(cart != null)
  {
    var check = true;
    for(let i=0;i<cart.length;i++)
    {
        if(cart[i] == Number(itemclone.id))
        {
          check = false;          
        }
           
    }
    if(check)
    {
      cart.push(Number(itemclone.id));  
    }
  }
  else
  {
    cart = [Number(itemclone.id)];
  }

  localStorage.setItem('currentCart', JSON.stringify(cart));
  openCart();
}

function openCart()
{
  loaditemfromCart();
  $('#mainCart').show();
 
}

function sendOrder()
{
  if(localStorage.getItem('customer') == null)
  {
    alert('Please login!');
    return;
  }
  //aaaaaaaaaaaaaaaaaaaaaaaaaaa
    let sendMoney = Number($('#money').text().replace('$',''));
    var productArr = [];
    $(".productInBasket").each(function(){
      let ItemQty = $(this).find(".quantityInBasket").val();
      let ItemId = $(this).find(".quantityInBasket").attr("id");
      console.log(ItemId)
      let productObj = {'ItemId': Number(ItemId), 'ItemQty': Number(ItemQty)};
      productArr.push(productObj);
    });
    localStorage.setItem('currentproductArr',productArr);

  let cart = JSON.parse(localStorage.getItem('currentCart'));
  let customer = JSON.parse(localStorage.getItem('customer'));
  let customerId = customer.customerId;
  let restaurant = JSON.parse(localStorage.getItem('currentRestaurant'));
  let currentrestaurantId = restaurant.currentRestaurantId;
  console.log(sendMoney)
  model = {
    "billingItem": {
      "customerId": Number(customerId),
      "restaurantId": Number(currentrestaurantId),
      "billAmout": Number(sendMoney),
      "orderDate": "2021-04-27T08:21:42.975Z",
      "orderDetail": productArr
    }
  };
  fetch('http://localhost:4000/api/billing/createOrder', {
      method: 'POST',
      headers: {
          'Content-Type': 'application/json',
      },
      body: JSON.stringify(model),
  })
  .then(response => response.json())
  .then(res => {
    var result = JSON.stringify(res);
  }).catch((error) => {
    //alert('Too many item in Cart');
    console.error('Error:', error);
});
}

// function calculateTotalAmount(){
//   console.log('listItem:',listItems )
//   let result = 0;
//   $(".quantityInBasket").each(function(){
//     let ItemQty = $(this).val();
//     let ItemId = $(this).find(".quantityInBasket").attr("id");
//     // console.log(ItemId)
//     let item = listItems.find(e => e.itemId == ItemId);
//     // console.log(ItemId)
//     if (item) {
//       result += ItemQty * item.itemPrice;
//     }
//   });

//   console.log()
//   return result;
// }

// function generateTotalText(){
//   let amount = calculateTotalAmount();
//   $('#money').html('$'+ amount);
// }