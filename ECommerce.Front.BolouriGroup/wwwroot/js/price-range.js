$((function () {
    $("#slider-range").slider({
        range: !0, min: 0, max: 500, values: [50, 350], slide: function (e, a) {
            $("#amount").val( a.values[0] + " تومان " + " - " + a.values[1] + "  تومان " )
        }
    }), $("#amount").val(  $("#slider-range").slider("values", 0) + " تومان " + " - " + $("#slider-range").slider("values", 1) + "  تومان")
}));