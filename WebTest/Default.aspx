<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebTest.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="Scripts/jquery-3.3.1.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
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
    </form>
    <script type="text/javascript">
        $(function () {
            $.get("/GetData.ashx")
            .done(function (data) {
                data = JSON.parse(data);
                $(data).each(function () {
                    var tmp = JSON.parse(this);
                    var list = $(tmp.fields);
                    var mainInfo = $(list.filter(function () {
                        return this.hasOwnProperty("200");
                    })[0]["200"].subfields);
                    var title = mainInfo.filter(function () {
                        return this["a"];
                    })[0]["a"],
                    author = mainInfo.filter(function () {
                        return this.hasOwnProperty("f");
                    })[0]["f"];
                    $("tbody").append('<tr><td>' + title + '</td><td>' + author + '</td></tr>');
                });

            });
        });
    </script>
</body>
</html>
