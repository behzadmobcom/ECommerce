﻿/*
 Copyright (c) 2003-2013, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.html or http://ckeditor.com/license
*/
CKEDITOR.dialog.add("symbol", function(j) {
    function o(a, b, c) {
        if (null != a) {
            for (var s = CKEDITOR.tools.getNextId() + "_symbol_table_label", i = [""], h = 0, d = 0, j = !1, f, g, k = a.split(","), l = 0; l < k.length; l++) {
                f = 0;
                a = -1;
                g = !1;
                var e = k[l].split("-");
                switch (e.length) {
                case 0:
                    break;
                case 1:
                    "*" == e[0].charAt(e[0].length - 1) ? (a = f = parseInt(e[0].substring(0, e[0].length - 1), 16), g = !0) : a = f = parseInt(e[0], 16);
                    break;
                default:
                ("*" == e[0].charAt(e[0].length - 1) ? (f = parseInt(e[0].substring(0, e[0].length - 1), 16), g = !0) : f = parseInt(e[0], 16), "*" == e[1].charAt(e[1].length -
                    1)) ? (a = parseInt(e[1].substring(0, e[1].length - 1), 16), g = !0) : a = parseInt(e[1], 16);
                }
                h += a - f + 1;
                if (a >= f) {
                    j || (i.push("<table role=\"listbox\" aria-labelledby=\"" + s + "\" style=\"width: 100%; height: 100%; border-collapse: separate;\" align=\"center\" cellspacing=\"2\" cellpadding=\"2\" border=\"0\">"), j = !0);
                    for (; f <= a;) {
                        0 == d && i.push("<tr>");
                        for (var e = !0, n = d; n < c; n++, f++)
                            if (f <= a) {
                                var o = t(f), p = q && q[f] || o, r = "cke_symbol_label_" + f + "_" + CKEDITOR.tools.getNextNumber();
                                i.push("<td class=\"cke_dark_background\" style=\"cursor: default\" role=\"presentation\"><a href=\"javascript: void(0);\" role=\"option\" aria-posinset=\"" +
                                (f + 1) + "\"", " aria-setsize=\"" + h + "\"", " aria-labelledby=\"" + r + "\"", " style=\"cursor: inherit; display: block; height: 1.25em; margin-top: 0.25em; text-align: center;\" title=\"", CKEDITOR.tools.htmlEncode(p), "\" onkeydown=\"CKEDITOR.tools.callFunction( " + u + ", event, this )\" onclick=\"CKEDITOR.tools.callFunction(" + v + ", this); return false;\" tabindex=\"-1\"><span style=\"margin: 0 auto;cursor: inherit\"" + (g ? ">" + m : ">") + o + "</span><span class=\"cke_voice_label\" id=\"" + r + "\">" + p + "</span></a>");
                                i.push("</td>");
                            } else {
                                d = n;
                                e = !1;
                                break;
                            }
                        if (e)d = 0, i.push("</tr>");
                        else break;
                    }
                }
            }
            if (0 != d) {
                for (f = d; f < c; f++)i.push("<td>&nbsp;</td>");
                i.push("</tr>");
            }
            j ? (i[0] = 255 < h ? "<div style=\"width:100%;height:300px;overflow:auto;\">" : "<div>", i.push("</table></div>"), c = i.join(""), b.getElement().setHtml(c)) : b.getElement().setHtml("<div>No valid range(s) defined...</div>");
        } else b.getElement().setHtml("<div>No range defined...</div>");
    }

    function t(a) {
        a = a.toString(16).toUpperCase();
        return 1 > a.length ? eval("\"\\u0000\"") : 2 > a.length ? eval("\"\\u000" + a + "\"") : 3 > a.length ?
            eval("\"\\u00" + a + "\"") : 4 > a.length ? eval("\"\\u0" + a + "\"") : eval("\"\\u" + a + "\"");
    }

    function w(a, b) { return a[0] == b[0] ? 0 : a[0] > b[0] ? 1 : -1 }

    var m = "&nbsp;",
        n = null,
        d,
        q = j.lang.symbol,
        k = null,
        p = function(a) {
            var b, a = a.data ? a.data.getTarget() : new CKEDITOR.dom.element(a);
            if ("a" == a.getName() && (b = a.getChild(0).getHtml()))a.removeClass("cke_light_background"), b.length > m.length && 0 == b.indexOf(m) && (b = b.substring(m.length)), d.hide(), a = j.document.createElement("span"), a.setHtml(b), j.insertText(a.getText());
        },
        v = CKEDITOR.tools.addFunction(p),
        l,
        g = function(a, b) {
            var c, b = b || a.data.getTarget();
            "span" == b.getName() && (b = b.getParent());
            if ("a" == b.getName() && (c = b.getChild(0).getHtml())) {
                l && h(null, l);
                var g = d.getContentElement("info", "unicodePreview").getElement();
                d.getContentElement("info", "charPreview").getElement().setHtml(c);
                var i = 0, i = "&nbsp;" == c ? 160 : c.length > m.length && 0 == c.indexOf(m) ? c.charCodeAt(m.length) : c.charCodeAt(0);
                g.setHtml("Unicode (hex): " + i + " (" + i.toString(16).toUpperCase() + ")");
                b.getParent().addClass("cke_light_background");
                l = b;
            }
        },
        h = function(a, b) {
            b = b || a.data.getTarget();
            "span" == b.getName() && (b = b.getParent());
            "a" == b.getName() && (d.getContentElement("info", "unicodePreview").getElement().setHtml("&nbsp;"), d.getContentElement("info", "charPreview").getElement().setHtml("&nbsp;"), b.getParent().removeClass("cke_light_background"), l = void 0);
        },
        u = CKEDITOR.tools.addFunction(function(a) {
            var a = new CKEDITOR.dom.event(a), b = a.getTarget(), c;
            c = a.getKeystroke();
            var d = "rtl" == j.lang.dir;
            switch (c) {
            case 38:
                if (c = b.getParent().getParent().getPrevious())
                    c =
                        c.getChild([b.getParent().getIndex(), 0]), c.focus(), h(null, b), g(null, c);
                a.preventDefault();
                break;
            case 40:
                if (c = b.getParent().getParent().getNext())if ((c = c.getChild([b.getParent().getIndex(), 0])) && 1 == c.type)c.focus(), h(null, b), g(null, c);
                a.preventDefault();
                break;
            case 32:
                p({ data: a });
                a.preventDefault();
                break;
            case d ? 37 : 39:
            case 9:
                if (c = b.getParent().getNext())c = c.getChild(0), 1 == c.type ? (c.focus(), h(null, b), g(null, c), a.preventDefault(!0)) : h(null, b);
                else if (c = b.getParent().getParent().getNext())
                (c = c.getChild([
                    0,
                    0
                ])) && 1 == c.type ? (c.focus(), h(null, b), g(null, c), a.preventDefault(!0)) : h(null, b);
                break;
            case d ? 39 : 37:
            case CKEDITOR.SHIFT + 9:
            (c = b.getParent().getPrevious()) ? (c = c.getChild(0), c.focus(), h(null, b), g(null, c), a.preventDefault(!0)) : (c = b.getParent().getParent().getPrevious()) ? (c = c.getLast().getChild(0), c.focus(), h(null, b), g(null, c), a.preventDefault(!0)) : h(null, b);
            }
        }),
        x = [CKEDITOR.dialog.cancelButton],
        y = j.lang.common.generalTab,
        z = j.lang.common.generalTab;
    j.config.sortSymbolRangesAlphabetically ? k || (k = CKEDITOR.config.symbolRanges.slice(),
        k.sort(w)) : k = CKEDITOR.config.symbolRanges;
    return{
        title: "Select Symbol",
        buttons: x,
        charColumns: 16,
        onShow: function() {
            var a = this.getContentElement("info", "symbolRange").getInputElement().$;
            console.log("domElement.childNodes.length=" + a.childNodes.length);
            if (a.childNodes.length > 0) {
                var b = null;
                if (!n) {
                    b = "Basic Latin";
                    if (j.config.defaultSymbolRange)b = j.config.defaultSymbolRange;
                    console.log("startRange = " + b);
                }
                for (var c = 0, d = 0; d < a.childNodes.length; d++) {
                    console.log(d + " domElement.childNodes[i].text = " + a.childNodes[d].text);
                    if (a.childNodes[d].text == b || a.childNodes[d].value == n) {
                        c = d;
                        break;
                    }
                }
                a.selectedIndex = c;
                console.log("selectedIndex = " + a.selectedIndex);
                b = this.getContentElement("info", "charContainer");
                o(a.childNodes[c].value, b, this.definition.charColumns);
            }
        },
        contents: [
            {
                id: "info",
                label: y,
                title: z,
                padding: 0,
                align: "top",
                onHide: function() { d && d.destroy() },
                elements: [
                    {
                        type: "vbox",
                        align: "top",
                        width: "100%",
                        children: [
                            {
                                type: "select",
                                id: "symbolRange",
                                width: "100%",
                                items: k,
                                size: 1,
                                onChange: function(a) {
                                    console.log("Range changed:");
                                    console.log(a.sender.getValue());
                                    n = a.sender.getValue();
                                    var b = d.getContentElement("info", "charContainer");
                                    o(a.sender.getValue(), b, d.definition.charColumns);
                                }
                            }, {
                                type: "html",
                                id: "charContainer",
                                html: "",
                                onMouseover: g,
                                onMouseout: h,
                                focus: function() {
                                    var a = this.getElement().getElementsByTag("a").getItem(0);
                                    setTimeout(function() {
                                        a.focus();
                                        g(null, a);
                                    }, 0);
                                },
                                onShow: function() {
                                    var a = this.getElement().getElementsByTag("a").getItem(0);
                                    a != null && setTimeout(function() {
                                        a.focus();
                                        g(null, a);
                                    }, 0);
                                },
                                onLoad: function(a) { d = a.sender }
                            }, {
                                type: "hbox",
                                align: "top",
                                width: "100%",
                                children: [
                                    { type: "html", html: "<div></div>" }, { type: "html", id: "unicodePreview", className: "cke_dark_background", style: "border:1px solid #eeeeee;font-size:14px;height:20px;width:192px;padding-top:25px;font-family:'Microsoft Sans Serif',Arial,'Arial Unicode',Helvetica,Verdana;text-align:center;", html: "<div>&nbsp;</div>" }, {
                                        type: "html",
                                        id: "charPreview",
                                        className: "cke_dark_background",
                                        style: "border:1px solid #eeeeee;font-size:28px;height:40px;width:70px;padding-top:9px;font-family:'Microsoft Sans Serif',Arial,'Arial Unicode',Helvetica,Verdana;text-align:center;",
                                        html: "<div>&nbsp;</div>"
                                    }
                                ]
                            }
                        ]
                    }
                ]
            }
        ]
    };
});