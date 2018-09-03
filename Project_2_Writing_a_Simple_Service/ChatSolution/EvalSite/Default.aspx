<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
    <script type="text/javascript">

        function LookupCourses() {
            tempuri.org.ICourseService.getCourseList(OnLookupComplete, OnError);
        }

        function OnLookupComplete(result, state) {
            var sel = $get("Select1");
            for (i in result)
                sel.add(new Option(result[i], result[i]));
        }

        function OnError(result) { alert("Error: " + result.get_message()); }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- This will at runtime download a JavaScript code from the service that is capable of calling
            the service.-->
        <div>
            <asp:ScriptManager ID="ScriptManager1" runat="server">
                <Services>
                    <asp:ServiceReference Path="~/Courses.svc" />
                </Services>
            </asp:ScriptManager>
        </div>
    </form>
</body>
</html>
