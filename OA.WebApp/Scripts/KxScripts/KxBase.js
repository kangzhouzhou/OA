(function (win) {
    "use strict";
    if (!win.Kx)
        win.Kx = {}
    var divMask = null, divMsg = null, maskContainer = null, maskFlag = true;

    //初始化方法
    win.Kx.ready = function (initFunc) {
        if (!(initFunc instanceof Function)) {
            throw Error("init is not a function");
        }
        //兼容IE方式
        if (win.document.addEventListener) {
            //必须以调用的方式进行传递
            win.document.addEventListener("DOMContentLoaded", initFunc());
        } else {
            win.document.onreadystatechange = function () {
                if (win.document.readyState == "interactive" || win.document.readyState == "complete") {
                    win.document.onreadystatechange = null;
                    initFunc();
                }
            };
        }
    };
    //兼容浏览器问题
    win.Kx.crossEvent=function (event) {
        //兼容IE浏览器window.event
        var e = event || window.event;

        //兼容事件目标元素 IE-srcElement firefox-target chrome-target,srcElement
        if (!e.target) {
            e.target = e.srcElement;
        }

        //兼容取消默认行为 IE returnValue
        e.preventDefault = e.preventDefault || function () {           
            e.returnValue = false;
        }
        return e;
    }

    //蒙板层开启,参数需要进行蒙板的容器
    win.Kx.mask = function (inputMsg, container) {
        if (maskFlag) {
            maskFlag = false;
            maskContainer = container || window.document.body;
            if (!divMask || !divMsg) {
                divMask = document.createElement("div");
                divMask.style.backgroundColor = "gray";
                divMask.style.position = "absolute";
                divMask.style.left = "0px";
                divMask.style.top = "0px";

                divMsg = document.createElement("div");
                divMsg.style.textAlign = "center";
                divMsg.style.fontSize = "14px";
                divMsg.style.backgroundColor = "white";
                divMsg.style.position = "absolute";
                divMsg.style.borderWidth = "3px";
                divMsg.style.borderStyle = "solid";
                divMsg.style.padding = "10px 5px 10px 5px";
                divMsg.style.width = "200px";
                divMsg.style.top = "45%";
                divMsg.style.left = "40%";
            }
            divMask.style.height = maskContainer.offsetHeight + "px";
            divMask.style.width = maskContainer.offsetWidth + "px";
            divMask.style.display = "block";
            divMask.style.opacity = 0.3;
            divMask.style.zIndex = 9998;
            maskContainer.appendChild(divMask);

            var msg = inputMsg || "数据加载中,请稍等...";
            divMsg.innerHTML = msg;
            divMsg.style.display = "block";
            divMsg.style.zIndex = 9999;
            maskContainer.appendChild(divMsg);            
        }
    }

    //蒙板层关闭
    win.Kx.unMask = function () {
        if (!maskFlag) {
            divMsg.style.display = "none";
            divMask.style.display = "none";
            maskFlag = true;
        }
    }

    //判断是否注册插件,COM唯一标识或者Netscape名称
    win.Kx.checkPlugin = function (pluginName,comIdentify) {
        if (window.ActiveXObject) {
            try {
                var obj = new ActiveXObject(comIdentify);
                return true;
            } catch (e) {
                return false;
            }
        } else {
            for (var i = 0; i < navigator.plugins.length; i++) {
                if (navigator.plugins[i].name.toLowerCase().indexOf(name) != -1) {
                    return true;
                }
            }
            return false;
        }
    }
})(window);