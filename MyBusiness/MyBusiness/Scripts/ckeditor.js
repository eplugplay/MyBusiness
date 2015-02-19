﻿

function SavePage(tabName)
{   var ckeditor = null;
    if (tabName == "Home")
    {
        ckeditor = CKEDITOR.instances.editor1.getData();
    }
    else
    {
        ckeditor = CKEDITOR.instances.editor2.getData();
    }
    $("#popupwindow").dialog({
        autoOpen: false,
        modal: true,
        show: "fade",
        height: "300",
        width: "500px",
        title: "Update Page?",
        buttons: {
            "Update": function () {
                $.ajax({
                    type: "POST",
                    cache: false,
                    async: false,
                    url: "/Admin/UpdatePage",
                    dataType: "json",
                    data: { ckeditor: ckeditor, pagename: tabName },
                    async: false,
                    success: function (data) {
                        if (data == "Successful") {
                            LoadSecondaryMsg('Updated Successfully.');
                        }
                        else {
                            LoadSecondaryMsg('Did not update Successfully.');
                        }
                        $("#popupwindow").dialog("close");
                        return true;
                    }
                });
            },
            "Cancel": function () {
                $(this).dialog("close");
            }
        }
    });
    $("#popupwindow").html("Do you want to update this page?");
    $("#popupwindow").dialog("open");
    return true;
}