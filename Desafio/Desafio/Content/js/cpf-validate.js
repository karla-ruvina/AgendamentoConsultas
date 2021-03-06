(function (e) {
    function n(e) {
        if (e.match(/^\d{2}\.\d{3}\.\d{3}\/\d{4}\-\d{2}$/) != null) {
            var t = e.substring(0, 2);
            var n = e.substring(3, 6);
            var r = e.substring(7, 10);
            var i = e.substring(11, 15);
            var o = e.substring(16, 18);
            var u;
            var a;
            var f = true;
            a = t + n + r + i + o; s = a;
            c = s.substr(0, 12);
            var l = s.substr(12, 2);
            var h = 0;

            if (a == "00000000000000" || a == "11111111111111" || a == "22222222222222" || a == "33333333333333"
                || a == "44444444444444" || a == "55555555555555" || a == "66666666666666" || a == "77777777777777"
                || a == "88888888888888" || a == "99999999999999") f = false;

            for (u = 0; u < 12; u++)
                h += c.charAt(11 - u) * (2 + u % 8);

            if (h == 0)
                f = false;

            h = 11 - h % 11;

            if (h > 9)
                h = 0;

            if (l.charAt(0) != h)
                f = false;

            h *= 2;

            for (u = 0; u < 12; u++) {
                h += c.charAt(11 - u) * (2 + (u + 1) % 8);
            }

            h = 11 - h % 11;

            if (h > 9)
                h = 0;

            if (l.charAt(1) != h)
                f = false;

            return f
        } return false
    } function r(e) {
        if (e.match(/^\d{3}\.\d{3}\.\d{3}\-\d{2}$/) != null) {
            var t = e.substring(0, 3);
            var n = e.substring(4, 7);
            var r = e.substring(8, 11);
            var i = e.substring(12, 14);
            var o;
            var u;
            var a = true;
            u = t + n + r + i;
            s = u;
            c = s.substr(0, 9);
            if (u == "00000000000" || u == "11111111111" || u == "22222222222" || u == "33333333333"
                || u == "44444444444" || u == "55555555555" || u == "66666666666" || u == "77777777777"
                || u == "88888888888" || u == "99999999999") a = false;
            var f = s.substr(9, 2);
            var l = 0;

            for (o = 0; o < 9; o++) {
                l += c.charAt(o) * (10 - o)
            }
            if (l == 0)
                a = false;
            l = 11 - l % 11;

            if (l > 9)
                l = 0;

            if (f.charAt(0) != l)
                a = false;

            l *= 2;

            for (o = 0; o < 9; o++) {
                l += c.charAt(o) * (11 - o)
            }
            l = 11 - l % 11;
            if (l > 9)
                l = 0;
            if (f.charAt(1) != l)
                a = false;
            return a
        } return false
    } var t = null;
    e.fn.cpfcnpj = function (i) {
        var s = e.extend({
            mask: false,
            validate: "cpfcnpj",
            event: "focusout",
            handler: e(this),
            ifValid: null,
            ifInvalid: null
        }, i);
        if (s.mask) {
            if (jQuery().mask == null) {
                s.mask = false; console.log("jQuery mask not found.")
            } else {
                if (s.validate == "cpf") {
                    e(this).mask("000.000.000-00")
                } else if (s.validate == "cnpj") {
                    e(this).mask("00.000.000/0000-00")
                } else {
                    var o = e(this);
                    var u = {
                        onKeyPress: function (e) {
                            var t = ["000.000.000-00", "00.000.000/0000-00"];
                            msk = e.length > 14 ? t[1] : t[0]; o.mask(msk, this)
                        }
                    }; e(this).mask("000.000.000-00", u)
                }
            }
        } return this.each(function () {
            var i = null;
            var o = e(this);
            e(document).on(s.event, s.handler, function () {
                if (o.val().length == 14 || o.val().length == 18) {
                    if (s.validate == "cpf") {
                        i = r(o.val())
                    } else if (s.validate == "cnpj") {
                        i = n(o.val())
                    } else if (s.validate == "cpfcnpj") {
                        if (r(o.val())) { i = true; t = "cpf" }
                        else if (n(o.val())) { i = true; t = "cnpj" }
                        else i = false
                    }
                } else i = false;
                if (e.isFunction(s.ifValid)) {
                    if (i != null && i) {
                        if (e.isFunction(s.ifValid)) { var u = e.Callbacks(); u.add(s.ifValid); u.fire(o) }
                    } else if (e.isFunction(s.ifInvalid)) { s.ifInvalid(o) }
                }
            })
        })
    }
})(jQuery)