<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebTest.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
    <script src="Scripts/pagination.js"></script>
    <style type="text/css">
        .pagination{
            text-align:center;
            user-select:none;
        }
        .pagination li{
            display:inline-block;
            list-style:none;
            padding:2px 3px;
            margin:3px;
            border:1px solid #ccc;
        }
        .pagination li:not(.pg-on){
            cursor:pointer;
        }
        .pagination li.pg-on{
            background:#ccc;
        }
        .pagination li.pg-disabled{
            cursor:not-allowed;
        }
        span.btn{
            display:inline-block;
            width:40px;
            height:20px;
            padding:5px;
            margin:3px;
            border:1px solid #ccc;
            background:#dedede;
            color:#666;
            cursor:pointer;
        }
    </style>
</head>
<body>
 
    <div>
        <select>
            <option>标题</option>
            <option>作者</option>
            <option>ISBN</option>
        </select>
        <input type="text" id="searchKw" />
        <span class="btn">搜索</span>
    </div> 
    <div>
    <table>
        <thead>
            <tr>
                <td>标题</td>
                <td>作者</td>
            </tr>
        </thead>
        <tbody>
            
        </tbody>
    </table>
    </div>
    <div class="pagination">

    </div>
    <script type="text/javascript">
        $(function () {
            var getData = function (index) {
                $("tbody").empty();
                var type = $("select>option:selected").index(),
                    kw = $("#searchKw").val();
                $.get("/GetData.ashx", { kw: kw, type: type, pi: index, ps: 10 })
                .done(function (data) {
                    data = JSON.parse(data);
                    var count = data.count;
                    $(data.list).each(function () {
                        $("tbody").append('<tr><td>' + this.Title + '</td><td>' + this.Author + '</td><td>' + this.ISBN + '</td></tr>');
                        pagination({
                            container: $(".pagination")[0],
                            pageIndex: index,
                            pageSize: 10,
                            count: count,
                            fn: getData
                        });
                    });
                });
            };

            $("span.btn").on("click", function () {
                getData(1);
            });

        });
    </script>
</body>
</html>
