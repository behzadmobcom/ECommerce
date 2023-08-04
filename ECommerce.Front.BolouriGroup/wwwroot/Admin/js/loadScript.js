window.addEventListener("load", () => {
  const pathname = window.location.pathname;
  const currentListItem = $(`#kt_aside_menu_wrapper a[href='${pathname}']`);
  if (currentListItem.length === 1) {
    const currentMainListParent = currentListItem.parent().parent().parent().parent();
    currentMainListParent.toggleClass("menu-item-open");
    currentListItem.parent().css("background-color", "#2b2b3d");
  }
});
