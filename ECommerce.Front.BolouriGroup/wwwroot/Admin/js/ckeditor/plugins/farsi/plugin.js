var keyboardLayout = {};
var langCycle = [""];
var owneditor;
var IEChangeLangFlag;

(function() {
    var k = null;
    var titleTimeout = null;

    function showLang(langIndex) {
        var langTitle = "Language: ";
        for (var i = 0; i < langCycle.length; i++) {
            var lang = getLangFromCycle(i) || "default";
            langTitle += (i == langIndex ? "[" + lang + "]" : lang) + " ";
        }

        if (titleTimeout) clearTimeout(titleTimeout);
        document.oldTitle = document.oldTitle || document.title;
        document.title = langTitle;
        titleTimeout = setTimeout(function() {
            document.title = document.oldTitle;
            document.oldTitle = undefined;
            titleTimeout = null;
        }, 1000);
    }

    function getLangFromCycle(i) {
        return langCycle[i].lang ? langCycle[i].lang : langCycle[i];
    }

    function getDirectionFromCycle(i) {
        return langCycle[i].direction ? langCycle[i].direction : "";
    }

    function getNextLangIndex(targetLang) {
        var nextIndex = langCycle.length ? 0 : -1;
        for (var i = 0; i < langCycle.length; i++) {
            if (getLangFromCycle(i) == targetLang) {
                nextIndex = i < langCycle.length - 1 ? i + 1 : 0;
            }
        }
        return nextIndex;
    }

    function addEvent(elem, type, handle) {
        if (elem.addEventListener)
            elem.addEventListener(type, handle, false);
        else if (elem.attachEvent)
            elem.attachEvent("on" + type, handle);
        else
            elem["on" + type] = handle;
    }

    function isTextField(elem) {
        return (elem.tagName == "TEXTAREA") ||
        (elem.tagName == "INPUT" && elem.type && elem.type.toUpperCase() == "TEXT");
    }


    function IE_OnKeyDown(e) {
        if (e) k = e.which;
        IEChangeLangFlag = false;
        if (e.ctrlKey && e.keyCode == 0x0020) {
            IEChangeLangFlag = true;
            IE_OnKeyPress(e);
        }
    }

    function IE_OnKeyPress(e) {
        var target = e.target || e.srcElement;
        //if (!isTextField(target)) return;
        var key = e.which || e.charCode || e.keyCode;

        //read layout language from lang attribute
        var lang = (owneditor.langCode || "").match(/^[a-zA-Z]{2}/) || "";
        if (!lang) {
            if (!langCycle.length) return true;
            lang = getLangFromCycle(0);
        }
        // Ctrl+Space changes language (just if there's any cycle available)
        if (IEChangeLangFlag && langCycle && langCycle.length > 1) {
            var nextLangIndex = getNextLangIndex(lang);
            if (nextLangIndex >= 0) {
                showLang(nextLangIndex);
                owneditor.langCode = getLangFromCycle(nextLangIndex);
            }
            if (e.preventDefault) e.preventDefault();
            e.returnValue = false;
            return false;
        }
        var layout = keyboardLayout[lang];
        if ((!layout) ||
        (e.ctrlKey || e.altKey || e.metaKey) ||
        (k != 0x0020 && k < 0x0030) || //fix for opera
        (key < 0x0020 || key > 0x007F))
            return true;

        var value =
        (key == 0x0020 && e.shiftKey && layout[0x005f]) || //Shift+Space may be defined in layout[0x5f]
        (layout[key - 0x0020]);

        value = typeof value == "string" ?
            value :
            String.fromCharCode(value);

        var ds = document.selection,
            ss = target.selectionStart;

        if (typeof ss == "number") { //standard browsers: http://www.w3.org/TR/html5/editing.html#dom-textarea-input-selectionstart
            var sl = target.scrollLeft, st = target.scrollTop; //fix for firefox
            target.value = target.value.substring(0, ss) + value + target.value.substring(target.selectionEnd, target.value.length);
            var sr = ss + value.length;
            target.setSelectionRange(sr, sr);
            target.scrollLeft = sl;
            target.scrollTop = st;
        } else if (ds) { //IE: http://msdn.microsoft.com/en-us/library/ms535869(VS.85).aspx
            var r = ds.createRange();
            r.text = value;
            r.setEndPoint("StartToEnd", r);
            r.select();
        } else { //unknown browsers
            owneditor.insertText(value);
        }

        if (e.preventDefault) e.preventDefault();
        e.returnValue = false;
        return false;
    }

    var Other_OnKeyDown = function(e) {
        if (e) k = e.which;
    };

    var Other_OnKeyPress = function(e) {
        var target = e.target || e.srcElement;
        //if (!isTextField(target)) return;
        var key = e.which || e.charCode || e.keyCode;

        //read layout language from lang attribute
        var lang = (owneditor.langCode || "").match(/^[a-zA-Z]{2}/) || "";
        if (!lang) {
            if (!langCycle.length) return true;
            lang = getLangFromCycle(0);
        }

        // Ctrl+Space changes language (just if there's any cycle available)
        if (e.ctrlKey && key == 0x0020 && langCycle && langCycle.length > 1) {
            var nextLangIndex = getNextLangIndex(lang);
            if (nextLangIndex >= 0) {
                showLang(nextLangIndex);
                owneditor.langCode = getLangFromCycle(nextLangIndex);
            }
            if (e.preventDefault) e.preventDefault();
            e.returnValue = false;
            return false;
        }
        var layout = keyboardLayout[lang];
        if ((!layout) ||
        (e.ctrlKey || e.altKey || e.metaKey) ||
        (k != 0x0020 && k < 0x0030) || //fix for opera
        (key < 0x0020 || key > 0x007F))
            return true;

        var value =
        (key == 0x0020 && e.shiftKey && layout[0x005f]) || //Shift+Space may be defined in layout[0x5f]
        (layout[key - 0x0020]);

        value = typeof value == "string" ?
            value :
            String.fromCharCode(value);

        var ds = document.selection,
            ss = target.selectionStart;

        if (typeof ss == "number") { //standard browsers: http://www.w3.org/TR/html5/editing.html#dom-textarea-input-selectionstart
            var sl = target.scrollLeft, st = target.scrollTop; //fix for firefox
            target.value = target.value.substring(0, ss) + value + target.value.substring(target.selectionEnd, target.value.length);
            var sr = ss + value.length;
            target.setSelectionRange(sr, sr);
            target.scrollLeft = sl;
            target.scrollTop = st;
        } else if (ds) { //IE: http://msdn.microsoft.com/en-us/library/ms535869(VS.85).aspx
            var r = ds.createRange();
            r.text = value;
            r.setEndPoint("StartToEnd", r);
            r.select();
        } else { //unknown browsers
            owneditor.insertText(value);
        }

        if (e.preventDefault) e.preventDefault();
        e.returnValue = false;
        return false;
    };
    CKEDITOR.plugins.add("farsi",
    {
        init: function(editor) {

            editor.on("contentDom", function(e) {

                var doc = editor.document.$;
                owneditor = editor;
                if (CKEDITOR.env.ie) { // If Internet Explorer.
                    doc.attachEvent("onkeydown", IE_OnKeyDown);
                    doc.attachEvent("onkeypress", IE_OnKeyPress);
                } else { // If Gecko.
                    doc.addEventListener("keydown", Other_OnKeyDown, true);
                    doc.addEventListener("keypress", Other_OnKeyPress, true);
                }
            });

        } //Init
    });

})();
keyboardLayout["fa"] = [
    0x0020, 0x0021, 0x061b, 0x066b, 0xfdfc, 0x066a, 0x060c, 0x06af, 0x0029, 0x0028, 0x002a, 0x002b, 0x0648, 0x002d, 0x002e, 0x002f,
    0x06f0, 0x06f1, 0x06f2, 0x06f3, 0x06f4, 0x06f5, 0x06f6, 0x06f7, 0x06f8, 0x06f9, 0x003a, 0x06a9, 0x003e, 0x003d, 0x003c, 0x061f,
    0x066c, 0x0624, 0x200c, 0x0698, 0x064a, 0x064d, 0x0625, 0x0623, 0x0622, 0x0651, 0x0629, 0x00bb, 0x00ab, 0x0621, 0x0654, 0x005d,
    0x005b, 0x0652, 0x064b, 0x0626, 0x064f, 0x064e, 0x0670, 0x064c, 0x0653, 0x0650, 0x0643, 0x062c, 0x005c, 0x0686, 0x00d7, 0x0640,
    0x200d, 0x0634, 0x0630, 0x0632, 0x06cc, 0x062b, 0x0628, 0x0644, 0x0627, 0x0647, 0x062a, 0x0646, 0x0645, 0x067e, 0x062f, 0x062e,
    0x062d, 0x0636, 0x0642, 0x0633, 0x0641, 0x0639, 0x0631, 0x0635, 0x0637, 0x063a, 0x0638, 0x007d, 0x007c, 0x007b, 0x00f7, 0x200c
];
langCycle.push({ lang: "fa", direction: "rtl" });