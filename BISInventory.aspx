<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BISInventory.aspx.cs" Inherits="YourNamespace.BISInventory" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>BIS Inventory</title>
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <h1 align="center">BIS Inventory</h1>

            <br />

            <!-- Filter by Condition -->
            <div class="form-group">
                <asp:Label runat="server" Text="Filter By Condition" />
                <asp:DropDownList ID="ddlCondition" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlCondition_SelectedIndexChanged" DataSourceID="sqlCondition" DataTextField="Description" DataValueField="Id" CssClass="form-control"></asp:DropDownList>
                <asp:SqlDataSource ID="sqlCondition" runat="server" ConnectionString="<%$ ConnectionStrings:BISInventoryCOMP235-ConnectionString %>" SelectCommand="SELECT [Id], [Description] FROM [Condition]"></asp:SqlDataSource>
            </div>

            <!-- GridView to Display Inventory -->
            <asp:GridView ID="gvInventory" runat="server" AutoGenerateColumns="false" DataSourceID="sqlInventory" CssClass="table table-bordered" DataKeyNames="Id"
                OnRowEditing="gvInventory_RowEditing" OnRowUpdating="gvInventory_RowUpdating" OnRowCancelingEdit="gvInventory_RowCancelingEdit">
                <Columns>

                    <asp:BoundField DataField="Id" HeaderText="ID" ReadOnly="true" />


                    <asp:TemplateField HeaderText="Serial Number">
                        <ItemTemplate>
                            <asp:Label ID="lblSerialNo" runat="server" Text='<%# Eval("SerialNo") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtSerialNo" runat="server" Text='<%# Bind("SerialNo") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Item Name">
                        <ItemTemplate>
                            <asp:Label ID="lblItemName" runat="server" Text='<%# Eval("ItemName") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtItemName" runat="server" Text='<%# Bind("ItemName") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Date Purchased">
                        <ItemTemplate>
                            <asp:Label ID="lblPurchased" runat="server" Text='<%# Eval("Purchased", "{0:yyyy-MM-dd}") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtPurchased" runat="server" Text='<%# Bind("Purchased", "{0:yyyy-MM-dd}") %>' TextMode="Date"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Image">
                        <ItemTemplate>
                            <asp:Image ID="imgItem" runat="server" ImageUrl='<%# Eval("ImagePath") %>' Width="100" />
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:FileUpload ID="fuImage" runat="server" />
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:TemplateField HeaderText="Condition Rating">
                        <ItemTemplate>
                            <asp:Label ID="lblCondition" runat="server" Text='<%# Eval("condition") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlCondition" runat="server" DataSourceID="sqlCondition" DataTextField="Description" DataValueField="Id" SelectedValue='<%# Bind("condition") %>'></asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>


                    <asp:CommandField ShowEditButton="true" />

                    <asp:CommandField ShowDeleteButton="true" />
                </Columns>
            </asp:GridView>

            <asp:SqlDataSource ID="sqlInventory" runat="server" ConnectionString="<%$ ConnectionStrings:BISInventoryCOMP235-ConnectionString %>"
                SelectCommand="SELECT * FROM Inventory WHERE condition = @condition"
                UpdateCommand="UPDATE Inventory SET SerialNo = @SerialNo, ItemName = @ItemName, Purchased = @Purchased, ImagePath = @ImagePath, condition = @condition WHERE Id = @Id"
                InsertCommand="INSERT INTO Inventory (SerialNo, ItemName, Purchased, ImagePath, condition) VALUES (@SerialNo, @ItemName, @Purchased, @ImagePath, @condition)"
                DeleteCommand="DELETE FROM Inventory WHERE Id = @Id">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlCondition" Name="condition" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
                <UpdateParameters>
                    <asp:Parameter Name="SerialNo" Type="String" />
                    <asp:Parameter Name="ItemName" Type="String" />
                    <asp:Parameter Name="Purchased" Type="DateTime" />
                    <asp:Parameter Name="ImagePath" Type="String" />
                    <asp:Parameter Name="condition" Type="Int32" />
                    <asp:Parameter Name="Id" Type="Int32" />
                </UpdateParameters>
                <DeleteParameters>
                    <asp:Parameter Name="Id" Type="Int32" />
                </DeleteParameters>
            </asp:SqlDataSource>

            <!-- Add New Item Form -->
            <h2>Add New Item</h2>
            <div class="form-group">
                <asp:Label runat="server" Text="Serial Number" />
                <asp:TextBox ID="txtSerialNo" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Item Name" />
                <asp:TextBox ID="txtItemName" runat="server" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Date Purchased" />
                <asp:TextBox ID="txtPurchased" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Condition" />
                <asp:DropDownList ID="ddlNewCondition" runat="server" DataSourceID="sqlCondition" DataTextField="Description" DataValueField="Id" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="form-group">
                <asp:Label runat="server" Text="Image" />
                <asp:FileUpload ID="fuImage" runat="server" CssClass="form-control" />
            </div>
            <asp:Button ID="btnAdd" runat="server" Text="Add to Inventory" OnClick="btnAdd_Click" CssClass="btn btn-primary" />
            <asp:Label ID="lblMessage" runat="server" CssClass="text-danger"></asp:Label>
        </div>
    </form>
</body>
</html>
