using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web;
using System.Configuration;
using System.IO;

namespace YourNamespace
{
    public partial class BISInventory : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ddlCondition.DataBind();
                gvInventory.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (fuImage.HasFile)
            {
                string fileExtension = Path.GetExtension(fuImage.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                if (Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    string imagePath = "~/Images/" + fuImage.FileName;
                    fuImage.SaveAs(Server.MapPath(imagePath));

                    // Insert new item into the database
                    sqlInventory.InsertParameters.Clear();
                    sqlInventory.InsertParameters.Add("SerialNo", txtSerialNo.Text);
                    sqlInventory.InsertParameters.Add("ItemName", txtItemName.Text);
                    sqlInventory.InsertParameters.Add("Purchased", txtPurchased.Text);
                    sqlInventory.InsertParameters.Add("ImagePath", imagePath); // Save the image path
                    sqlInventory.InsertParameters.Add("condition", ddlNewCondition.SelectedValue);
                    sqlInventory.Insert();

                    lblMessage.Text = "Item added successfully!";
                    gvInventory.DataBind();
                }
                else
                {
                    lblMessage.Text = "Invalid file type. Only .jpg, .jpeg, .png, and .gif are allowed.";
                }
            }
            else
            {
                lblMessage.Text = "Please select an image to upload.";
            }
        }

        protected void ddlCondition_SelectedIndexChanged(object sender, EventArgs e)
        {
            gvInventory.DataBind();
        }

        // Event handler for RowEditing
        protected void gvInventory_RowEditing(object sender, GridViewEditEventArgs e)
        {
            // Set the GridView to edit mode
            gvInventory.EditIndex = e.NewEditIndex;
            gvInventory.DataBind();
        }

        // Event handler for RowUpdating
        protected void gvInventory_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            // Get the updated values from the GridView
            string serialNo = (gvInventory.Rows[e.RowIndex].FindControl("txtSerialNo") as TextBox)?.Text;
            string itemName = (gvInventory.Rows[e.RowIndex].FindControl("txtItemName") as TextBox)?.Text;
            DateTime purchased = DateTime.Parse((gvInventory.Rows[e.RowIndex].FindControl("txtPurchased") as TextBox)?.Text);
            FileUpload fuImage = (gvInventory.Rows[e.RowIndex].FindControl("fuImage") as FileUpload); // Get the FileUpload control
            string imagePath = "";

            // Check if a new image is uploaded
            if (fuImage.HasFile)
            {
                string fileExtension = Path.GetExtension(fuImage.FileName).ToLower();
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

                if (Array.Exists(allowedExtensions, ext => ext == fileExtension))
                {
                    // Save the new image
                    imagePath = "~/Images/" + fuImage.FileName;
                    fuImage.SaveAs(Server.MapPath(imagePath));
                }
                else
                {
                    lblMessage.Text = "Invalid file type. Only .jpg, .jpeg, .png, and .gif are allowed.";
                    return;
                }
            }
            else
            {
                // If no new image is uploaded, keep the existing image path
                imagePath = (gvInventory.Rows[e.RowIndex].FindControl("lblImagePath") as Label)?.Text;
            }

            int condition = int.Parse((gvInventory.Rows[e.RowIndex].FindControl("ddlCondition") as DropDownList)?.SelectedValue);

            // Update the database
            sqlInventory.UpdateParameters.Clear();
            sqlInventory.UpdateParameters.Add("SerialNo", serialNo);
            sqlInventory.UpdateParameters.Add("ItemName", itemName);
            sqlInventory.UpdateParameters.Add("Purchased", purchased.ToString("yyyy-MM-dd"));
            sqlInventory.UpdateParameters.Add("ImagePath", imagePath); // Updated image path
            sqlInventory.UpdateParameters.Add("condition", condition.ToString());
            sqlInventory.UpdateParameters.Add("Id", gvInventory.DataKeys[e.RowIndex].Value.ToString());
            sqlInventory.Update();

            // Exit edit mode
            gvInventory.EditIndex = -1;
            gvInventory.DataBind();
        }

        // Event handler for RowCancelingEdit
        protected void gvInventory_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            // Cancel edit mode
            gvInventory.EditIndex = -1;
            gvInventory.DataBind();
        }

        protected void gvInventory_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Get the ID of the record to delete
            int id = Convert.ToInt32(gvInventory.DataKeys[e.RowIndex].Value);

            // Delete the record from the database
            sqlInventory.DeleteParameters.Clear();
            sqlInventory.DeleteParameters.Add("Id", id.ToString());
            sqlInventory.Delete();

            // Refresh the GridView
            gvInventory.DataBind();
        }
    }
}