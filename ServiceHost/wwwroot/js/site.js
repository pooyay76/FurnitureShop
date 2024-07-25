 // Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.
// Write your JavaScript code. ss
var SinglePage = {};
const MaxFileSizeMb = 12;
const AllowedExtensions = ["jpg", "jpeg", "png"];

var ShowModal = function () {
    $("#modal").modal("show");
}

SinglePage.EnableModal = function () {
    window.onhashchange = function () {
        var url = window.location.hash.toLowerCase();
        if (!url.startsWith("#showmodal="))
            return;
        url = url.split("showmodal=")[1];
        $.get(url, null, function (htmlResult) {
            $("#modal-wrapper").html(htmlResult);
            const container = document.getElementById("modal-wrapper");
            const forms = container.getElementsByTagName("form");
            const newForm = forms[forms.length - 1];
            $.validator.unobtrusive.parse(newForm);
            ShowModal();
            $("#modal").on("shown.bs.modal", function () {

                window.location.hash = "##";

                $('.persian-date-input').persianDatepicker({
                    format: 'YYYY/MM/DD',
                    autoClose: true
                });

            });
        });
    };
};

$(document).ready(function () {
    SinglePage.EnableModal();
    updateShoppingCartView();
});

$(document).on("submit", 'form[data-ajax="true"]', function (e) {
    e.preventDefault();
    const form = $(this);
    const method = form.attr("method").toLocaleLowerCase();
    const targetUrl = form.attr("action");
    const action = form.attr("data-action");
    if (method === "get") {
        form = form.serializeArray();
        $.get(targetUrl, data, function (response) { CallBackHandler(response, action, form) });
    }
    else if (method === "post") {
        var data = new FormData(this);
        $.ajax({
            url: targetUrl,
            type: "post",
            data: data,
            enctype: "multipart/form-data",
            dataType: "json",
            processData: false,
            contentType: false,
            success: function (response) {
                CallBackHandler(response, action, form)
            },
            error: function (response) { alert(response.Message) }
        });
    }
});

function removeFromCart(productInventoryId) {
    CartService.RemoveOrder(productInventoryId);
    updateShoppingCartView();
}

function addToCart() {
    let orderQuantity = $("#quantityInput").val();
    let productInventoryId = $("#productInventoryIdInput").val();
    let productName = $("#productNameInput").val();
    let productPicture = $("#productPictureInput").val();
    let productPrice = $("#productPriceInput").val();
    CartService.AddOrder(productInventoryId, productName, orderQuantity, productPicture, productPrice);
    updateShoppingCartView();
}


function updateShoppingCartView() {
    orders = CartService.GetOrders();

    //This region handles shopping cart order list
        $("#cart_order_count").text(orders.length);
        let orderListWrapper = $("#cart-wrapper");
        orderListWrapper.html("");
        orders.forEach(product => {
            orderListWrapper.append(`
                                            <div class="single-cart-item">
                                                <a class="remove-icon" onclick="removeFromCart(${product.productInventoryId})">
                                                    <i class="ion-android-close"></i>
                                                </a>
                                                <div class="image">
                                                    <a href="#">
                                                        <img src="/Uploads/Product/${product.productPicture}"
                                                             class="img-fluid" alt="">
                                                    </a>
                                                </div>
                                                <div class="content">
                                                    <p class="product-title">
                                                        <a href="#">${product.productName}</a>
                                                    </p>
                                                    <p class="count">تعداد: ${product.orderQuantity}</p>
                                                    <p class="count">قیمت واحد: ${parseInt(product.productPrice)} تومان</p>
                                                </div>
                                            </div>
                                            `
            );
        });


    //This region handles cart cost calculation and billing
        cartCost = 0;
        //shippingCost and taxCost are in testing stage
        shippingCost = 20000;
        taxRate = 0.005;
        let cartCostElement = $("#cart-cost");
        let shippingCostElement = $("#shipping-cost");
        let taxCostElement = $("#tax-cost");
        let totalCostElement = $("#total-cost");
        orders.forEach(x => {
            cartCost += (parseInt(x.productPrice) * parseInt(x.orderQuantity))
        });
    let taxCost = cartCost * taxRate;
    cartCostElement.text(cartCost);
    shippingCostElement.text(shippingCost) ;
    taxCostElement.text(taxCost);
    totalCostElement.text(cartCost + shippingCost + taxCost) ;


}
class CartService {
    static cookieId = "cart-items";
    static GetOrders() {
        let orders = $.cookie(this.cookieId);
        if (orders === undefined)
            orders = [];
        else
            orders = JSON.parse(orders);
        return orders;
    }

    static AddOrder(productInventoryId, productName, orderQuantity, productPicture, productPrice, productSlug) {

        let newOrder = { productInventoryId, orderQuantity, productName, productPicture, productPrice, productSlug };

        //get existing orders
        let orders = this.GetOrders();
        //if orders exist, look for similar order, if it wasnt found then create new order,else increase qunatity
        let existingOrder = orders.find(order => order.productInventoryId == productInventoryId);
        if (existingOrder === undefined) {
            orders.push(newOrder);
            $.cookie(this.cookieId, JSON.stringify(orders), { expires: 2, path: "/" });
        }
        else {
            //if order exists, update quantity and price and set to cart items
            existingOrder.orderQuantity = parseInt(existingOrder.orderQuantity) + parseInt(orderQuantity);
            existingOrder.productPrice = productPrice;
            $.cookie(this.cookieId, JSON.stringify(orders), {expires:2 , path:"/"});
        }
    }
    static RemoveOrder(productInventoryId) {
        let orders = this.GetOrders();
        const index = orders.find(x => x.productInventoryId == productInventoryId);
        orders.splice(index, 1);
        $.cookie(this.cookieId, JSON.stringify(orders), { expires: 2, path: "/" });
    }

    static ReduceOrder(productInventoryId, orderQuantity) {
        let orders = this.GetOrders();
        orders.find(x => x.productInventoryId == productInventoryId).orderQuantity -= parseInt(orderQuantity);
        $.cookie(this.cookieId, JSON.stringify(orders), { expires: 2, path: "/" });
    }

    static IncreaseOrder(productInventoryId, orderQuantity) {
        let orders = this.GetOrders();
        orders.find(x => x.productInventoryId == productInventoryId).orderQuantity += parseInt(orderQuantity);
        $.cookie(this.cookieId, JSON.stringify(orders), { expires: 2, path: "/" });
    }
}



function CallBackHandler(data, action, form) {
    switch (action) {
        case "message":
            alert(data.Message);
            break;
        case "refresh":
            if (data.isSucceeded) {
                window.location.reload();
            }
            else {
                alert(data.Message)
            }
            break;
        case "relist":
            hideModal();
            const refreshUrl = form.attr("data-refreshurl");
            const refreshDiv = form.attr("data-refreshdivbyid");
            get(refereshUrl, refereshDiv);
            break;
        case "setValue":
    
        const element = form.data("element");
        $(`#${element}`).html(data);
        break;
        default: 
}
}

function get(url, refreshDiv) {
    const searchModel = window.location.search;
    $.get(url,
        searchModel,
        function (result) {
            $("#" + refreshDiv).html(result);
        });
}
function makeSlug(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(convertToSlug(value));
}

var convertToSlug = function (str) {
    var $slug = '';
    const trimmed = $.trim(str);
    $slug = trimmed.replace(/[^a-z0-9-آ-ی-]/gi, '-').replace(/-+/g, '-').replace(/^-|-$/g, '');
    return $slug.toLowerCase();
};


function fillField(source, dist) {
    const value = $('#' + source).val();
    $('#' + dist).val(value);
}

jQuery.validator.addMethod("maxFileSize",
    function (value, element, params) {
        return element.files[0].size <= (MaxFileSizeMb * 1024 * 1024);
    });
jQuery.validator.unobtrusive.adapters.addBool("maxFileSize");

jQuery.validator.addMethod("allowedFileExtensions",
    function (value, element, params) {
        var extension = element.files[0].name.split(".").pop();
        return AllowedExtensions.includes(extension);
    }
);
jQuery.validator.unobtrusive.adapters.addBool("allowedFileExtensions");



