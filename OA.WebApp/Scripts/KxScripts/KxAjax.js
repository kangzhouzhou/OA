(function (win) {
    "use strict";
    if (!win.Kx)
        win.Kx = {};

    //惰性加载,创建XMLHttpRequest对象,进行Ajax交互
    win.Kx.getXHR = (function () {        
        if (win.XMLHttpRequest) {
            //Chrome、FirFox等
            return function () {
                return new XMLHttpRequest();
            };
        } else {
            //IE 6.0
            try {
                return function () {
                    return new ActiveXObject("MSXML2.XMLHttp.6.0");
                };
            } catch (e) {
                return function () {
                    return new ActiveXObject("MSXML2.XMLHttp.3.0");
                };
            }
        }
    })();

    //get TRUE是同步,FALSE是异步
    win.Kx.get = function (url, func, async) {
        var xmlR = Kx.getXHR();
        //判断是否为同步
        if (async==null)
            async = true;
        //注册事件处理程序,处理兼容性内问题必须在open之间注册事件
        xmlR.onreadystatechange = function () {
            if (xmlR.readyState == 4) {
                func(xmlR.response);
            }
        };
        //处理URL,防止由于URL格式问题
        if (url.indexOf("?") != -1) {
            var urlContent = url.split("?");
            var newUrl = urlContent[0] + "?";
            var urlArg = urlContent[1].split("&");
            for (var i = 0; i < urlArg.length; i++) {
                newUrl += encodeURIComponent(urlArg);
            }
            url=newUrl;
        }
        //GET方式不需要指定请求头的Content-Type内容
        xmlR.open("GET", url, async);
        xmlR.send(null);
    };
    
    //post
    win.Kx.post = function (url, data, func, async) {
        var xmlR = Kx.getXHR();
        if (async == null)
            async = true;
        xmlR.onreadystatechange = function () {
            if (xmlR.readyState == 4) {
                func(xmlR.response);
            }
        };
        //处理对象为键值对方式
        if (data!=null && (!(data instanceof String) && !(data instanceof FormData))) {
            var dataKeyValue = "";
            for (var item in data) {
                dataKeyValue += item + "=" + data[item] + "&";
            }
            if (dataKeyValue.length > 0) {
                data = dataKeyValue.substr(0, dataKeyValue.length - 1);
            }
        }
        xmlR.open("POST", url, async);

        //必须设置请求的,且必须在open之后,send之前,处理兼容性 
        //不能设置Content-Type,会自动识别
        if (!(data instanceof FormData)) {
            xmlR.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
        }
        xmlR.send(data);
    };

    //ajax
    win.Kx.ajax = function (url, setting) {
        var xmlR = Kx.getXHR();
        var argObj = {
            type: "GET",
            data: null,
            contentType: "application/x-www-form-urlencoded",
            async: false,
            beforeSend: function () { },
            error: function () { },
            success: function () { }
        };
        if (setting) {
            for (var item in setting) {
                if (argObj[item]) {
                    argObj[item] = setting[item];
                }
            }
        }
        xmlR.onreadystatechange = function () {
            if (xmlR.status == 200 & xmlR.readyState == 4) {
                argObj.success(xmlR.response);
            }
        };
        xmlR.open(argObj["type"], url, argObj["async"]);
        xmlR.setRequestHeader("Content-Type", argObj["contentType"]);
        xmlR.send(argObj["data"]);
    };
})(window);