/////////////////****StartWishlist****////////////////
function RemoveWish(id) {
    $.ajax({
        type: "Get",
        url: "/index?handler=RemoveWishList&id=" + id,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result.message);
            if (result.code === 0) {
                var elementId = "#wishlist-" + id;
                $(elementId).remove();
            }
        },
        failure: function (response) {
            alert(response);
        }
    });
}
/////////////////****EndWishlist****////////////////
/////////////////****StartShop****////////////////
var labels = $("label");

labels.hover(
    function () { $(this).css("color", "blue").prevUntil().css("color", "blue"); }
    , function () {
        $(this).css("color", "inherit").prevUntil().css("color", "inherit");
        var checkedNum = $("[name='Commissions'] .checked").length;
        if (checkedNum === 1) {
            $(this + " .rateTitle").html($("[name='Commissions'] .checked").attr("data-rate"));
        } else {
            $(this + " .rateTitle").html("");
        }

    }
);
/////////////////****EndShop****////////////////
/////////////////****StartProfile****////////////////
function ChangePassword() {
    var oldPass = $("#OldPass").val();
    var newPass = $("#NewPass").val();
    var newConPass = $("#NewConPass").val();
    $.ajax({
        type: "Get",
        url: "userProfile?handler=ChangePassword&newPass=" + encodeURIComponent(newPass) + "&newConPass=" + encodeURIComponent(newConPass) + "&oldPass=" + encodeURIComponent(oldPass),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            swal(result.message);
        },
        failure: function (response) {
            alert(response);
        }
    });
}
function setCities() {
    var stateId = $('#state').val();
    $.ajax({
        type: "Get",
        url: "Checkout?handler=CityLoad&id=" + stateId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#city").html(result);
        },
        failure: function (response) {
            alert(response);
        }
    });

}



/////////////////****EndProfile****////////////////
/////////////////****StartChekout****////////////////

function fillAddress(id) {
    var arrayOfArrays = @Html.Raw(Json.Serialize(Model.SendInformationList));

    var sendInformation = arrayOfArrays.find(obj => {
        return obj.id == id;
    });
    console.log(sendInformation);
    $("#SendInformation_Id").val(sendInformation.id);
    $("#SendInformation_RecipientName").val(sendInformation.recipientName);
    $("#SendInformation_Mobile").val(sendInformation.mobile);
    $("#SendInformation_PostalCode").val(sendInformation.postalCode);
    $("#SendInformation_Address").val(sendInformation.address);
    $("#state").val(sendInformation.stateId);
    setCities();
    $("#city").val(sendInformation.cityId);

}
function setCities() {
    var stateId = $('#state').val();
    $.ajax({
        type: "Get",
        url: "Checkout?handler=CityLoad&id=" + stateId,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            $("#city").html(result);
        },
        failure: function (response) {
            alert(response);
        }
    });

}
/////////////////****EndChekout****////////////////