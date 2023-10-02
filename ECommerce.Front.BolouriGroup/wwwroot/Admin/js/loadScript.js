window.addEventListener("load", () => {
  const pathname = window.location.pathname;
  const currentListItem = $(`#kt_aside_menu_wrapper a[href='${pathname}']`);
  const secondaryListItem = $(`#kt_header_menu a[href='${pathname}']`);
  if (currentListItem.length === 1) {
    const currentMainListParent = currentListItem.parent().parent().parent().parent();
    currentMainListParent.toggleClass("menu-item-open");
    currentListItem.parent().css("background-color", "#2b2b3d");
  }
  if (secondaryListItem.length === 1) {
    secondaryListItem.closest(".menu-item-submenu").toggleClass("menu-item-open");
    secondaryListItem.children().css("color", "#15a9cd");
    secondaryListItem.parent().css("background-color", "#0000000f");
  }
});
