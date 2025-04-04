window.mudBlazorInterop = {
    openMudMenu: function (menuId) {
        var menu = document.getElementById(menuId);
        if (menu) {
            menu.MudMenu.open(); // Open the MudMenu by calling the JS function
        }
    },
    closeMudMenu: function (menuId) {
        var menu = document.getElementById(menuId);
        if (menu) {
            menu.MudMenu.close(); // Close the MudMenu by calling the JS function
        }
    }
};