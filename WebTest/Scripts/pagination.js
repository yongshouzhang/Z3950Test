function pagination(obj) {
    /*pageIndex: index,
pageSize:  size,
count:     count,    
container: container,
fn   :     fn
     */
    if (!obj || typeof obj != "object") {
        return false;
    }
    var pageIndex = obj.pageIndex || 1,
        pageSize = obj.pageSize || 10,
        count = obj.count || 0,
        container = obj.container,
        callback = obj.fn || function () { };
    var pageCount = Math.ceil(count / pageSize);
    if (pageCount == 0) {
        return false;
    }
    if (pageCount < pageIndex) {
        return false;
    }
    /*�¼���*/
    function bindEvent() {
        //��һҳ�¼�
        $(container).find(">ul>.pg-prev").unbind("click").bind("click", function () {
            if (pageIndex <= 1) {
                return false;
            }
            if (typeof callback == "function") {
                pageIndex--;
                pageIndex = pageIndex < 1 ? 1 : pageIndex;
                obj.pageIndex = pageIndex;
                callback(pageIndex);
                pagination(obj);
            }
        });
        //��һҳ�¼�
        $(container).find(">ul>.pg-next").unbind("click").bind("click", function () {
            if (pageIndex == pageCount) {
                return false;
            }
            if (typeof callback == "function") {
                pageIndex++;
                pageIndex = pageIndex > pageCount ? pageCount : pageIndex;
                obj.pageIndex = pageIndex;
                callback(pageIndex);
                pagination(obj);
            }
        });
        $(container).find(">ul>li:not(.pg-more):not(.pg-prev):not(.pg-next):not(.pg-on)").unbind("click").bind("click", function () {
            pageIndex = +$(this).html();
            pageIndex = isNaN(pageIndex) ? 1 : pageIndex;
            obj.pageIndex = pageIndex;
            if (typeof callback == "function") {
                callback(pageIndex);
                pagination(obj);
            }
        });
    };

    /*����ʽ*/
    function printHead() {
        var html = [];
        html.push('<li class="pg-prev ' + (pageIndex == 1 ? "pg-disabled" : "") + '">上一页</li>');
        return html.join("");
    }
    function printBody() {
        var html = [];
        var render = function (num, start) {
            start = start || 1;
            for (var i = start; i <= num; i++) {
                html.push('<li class="' + (pageIndex == i ? "pg-on" : "") + '">' + i + '</li>');
            }
        }
        if (pageCount <= 7) {
            render(pageCount);
        } else {
            if (pageIndex < 4) {
                render(4);
                html.push('<li class="pg-more">...</li>');
                html.push('<li >' + pageCount + '</li>');
            } else {
                html.push('<li >1</li>');
                html.push('<li class="pg-more">...</li>');
                if (pageCount - pageIndex > 3) {
                    render(pageIndex + 1, pageIndex - 1);
                    html.push('<li class="pg-more">...</li>');
                    html.push('<li >' + pageCount + '</li>');
                } else {
                    render(pageCount, pageCount - 3);
                }
            }
        }
        return html.join("");
    }
    function printTail() {
        var html = [];
        html.push('<li class="pg-next ' + (pageIndex == pageCount ? "pg-disabled" : "") + '">下一页</li>');
        return html.join("");
    }
    function show() {
        container.innerHTML = '<ul>' + printHead() + printBody() + printTail() + '</ul>';
    }
    show();
    bindEvent();
}