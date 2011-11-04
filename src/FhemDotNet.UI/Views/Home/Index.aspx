<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IList<FhemDotNet.Domain.Thermostat>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FhemDotNet - Homepage</title>
</head>
<body>
    <form id="thermostats" name="thermostats">
        <table id="thermostatTable">
            <tr>
                <th>Thermostat Name</th>
                <th>Current Temperature</th>
                <th>Desired Temperature</th>
                <th>Mode</th>
            </tr>
            <% for (int index = 0; index < Model.Count; index++ ) { %>
                <tr id="<%= Model[index].Name%>">
                    <td class="thermostatName">
                        <%= Model[index].Name%> <%= Html.HiddenFor(x => Model[index].Name)%>
                    </td>
                    <td class="currentTemp">
                        <%= Model[index].CurrentTemperature%>
                    </td>
                    <td class="desiredTemp">
                        <%= Html.TextBoxFor(x => Model[index].DesiredTemperature)%>
                        <span class="pendingDesiredTemp"><%= Model[index].PendingDesiredTemp)%></span>
                    </td>
                    <td class="mode">
                        <%= Html.DropDownListFor(x => Model[index].Mode, new List<SelectListItem>
                            {new SelectListItem { Text = "Auto", Value = "Auto" },
                                new SelectListItem { Text = "Manual", Value = "Manu" }
                            })%>
                    </td>
                </tr>
            <% } %>
        </table>
        
        <input type="submit" value="Submit" />
    </form>
</body>
</html>
