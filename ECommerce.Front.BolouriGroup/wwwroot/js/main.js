$(window).on("load", function () {
  if ($("#preloader").length) {
    $("#preloader")
      .delay(100)
      .fadeOut("slow", function () {
        $(this).remove();
      });
  }
  const pathname = window.location.pathname;
  const shopCategory = $(`#shop-widget-category a[href='${pathname}'], #category-layout a[href='${pathname}'], #category-nav a[href='${pathname}']`);
  shopCategory.closest(".dropdown-list").slideToggle("slow");
  shopCategory.closest(".dropdown-list").prev().toggleClass("rotate-category-arrow-180");
  shopCategory.toggleClass("category-item-selected");
});
$(window).on("scroll", function () {
  if ($(this).scrollTop() > 130) {
    $(".header-part").addClass("active");
    $("#BlueLogoImage").show();
    $("#WhiteLogoImage").hide();
  } else {
    $(".header-part").removeClass("active");
    $("#BlueLogoImage").hide();
    $("#WhiteLogoImage").show();
  }
}),
  $(function () {
    $(".dropdown-link").on("click", function () {
      $(this).next().slideToggle("slow"), $(this).before().toggleClass("rotate-category-arrow-180");
    });
  }),
  $(".header-cate, .cate-btn").on("click", function () {
    $("body").css("overflow", "hidden"),
      $(".category-part").addClass("active"),
      $(".category-close").on("click", function () {
        $("body").css("overflow", "inherit"), $(".category-part").removeClass("active");
      });
  }),
  $(".header-user").on("click", function () {
    $("body").css("overflow", "hidden"),
      $(".mobile-nav").addClass("active"),
      $(".nav-close").on("click", function () {
        $("body").css("overflow", "inherit"), $(".mobile-nav").removeClass("active");
      });
  }),
  $(".header-cart, .cart-btn").on("click", function () {
    $("body").css("overflow", "hidden"),
      $(".cart-part").addClass("active"),
      $(".cart-close").on("click", function () {
        $("body").css("overflow", "inherit"), $(".cart-part").removeClass("active");
      });
  }),
  $(".coupon-btn").on("click", function () {
    $(this).hide(), $(".coupon-form").css("display", "flex");
  }),
  $(".header-src").on("click", function () {
    $(".header-form").toggleClass("active"), $(this).children(".far fa-search").toggleClass("far fa-times");
  }),
  $(".wish").on("click", function () {
    $(this).toggleClass("active");
  }),
  $(".action-plus").on("click", function () {
    var i = $(this).closest(".product-action").children(".action-input").get(0).value++,
      c = $(this).closest(".product-action").children(".action-minus");
    i > 0 && c.removeAttr("disabled");
  }),
  $(".action-minus").on("click", function () {
    2 == $(this).closest(".product-action").children(".action-input").get(0).value-- && $(this).attr("disabled", "disabled");
  }),
  $(".review-widget-btn").on("click", function () {
    $(this).next(".review-widget-list").toggle();
  }),
  $(".offer-select").on("click", function () {
    $(this).text("کپی!");
  }),
  $(".modal").on("shown.bs.modal", function (i) {
    $(".preview-slider, .thumb-slider").slick("setPosition"), $(".product-details-image").addClass("slider-opacity");
  }),
  $(".profile-card.contact").on("click", function () {
    $(".profile-card.active").removeClass("active"), $(this).addClass("active");
  }),
  $(".profile-card.address").on("click", function () {
    $(".profile-card.active").removeClass("active"), $(this).addClass("active");
  }),
  $(".payment-card.payment").on("click", function () {
    $(".payment-card.active").removeClass("active"), $(this).addClass("active");
  });
$("[data-countdown]").each(function () {
  var $this = $(this),
    finalDate = $(this).data("countdown");
  $this.countdown(finalDate, function (event) {
    $this.html(
      event.strftime(
        '<span class="days">%-D<small>روز</small></span><span class="hour">%-H<small>ساعت</small></span><span class="minutes">%M<small>دقیقه</small></span><span  class="second">%S<small>ثانیه</small></span>'
      )
    );
  });
});
$(window).on("scroll", function () {
  if ($(this).scrollTop() > 100) {
    $(".back-to-top").fadeIn("slow");
  } else {
    $(".back-to-top").fadeOut("slow");
  }
});
$(".back-to-top").on("click", function () {
  $("html, body").animate({ scrollTop: 0 }, 1500);
});
