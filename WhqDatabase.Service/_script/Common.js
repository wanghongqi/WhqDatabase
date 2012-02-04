//ajax获取json对象
function AjaxToJson(/*路径*/url, /*数据*/data, /*异步/同步*/async, /*成功后回调函数*/callback) {
    $.ajax({
        url: url,
        data: data,
        type: 'post',
        dataType: 'text',
        contentType: 'text/json',
        async: async,
        dataFilter: function (data, type) {
            var d = data.replace(/"\\\/(Date\(.*?\))\\\/"/gi, 'new $1');
            return d;
        },
        success: function (data) {
            data = eval('(' + data + ')');
            callback(data.d);
        },
        error: function (xhr) {
            alert(xhr.responseText);
        }
    });
}