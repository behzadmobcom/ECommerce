jQuery((function(e) {
    var i = e(".faq-ans").hide();
    i.first().show(), e(".faq-que").on("click", (function() {
        var n = e(this);
        i.slideUp(), n.next().slideDown()
    }))
})), jQuery((function(e) {
    var i = e(".orderlist-body").hide();
    i.first().show(), e(".orderlist-head").on("click", (function() {
        var n = e(this);
        i.slideUp(), n.next().slideDown()
    }))
}));