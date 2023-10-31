const isShopPage = window.location.pathname.split("/")[1].toLowerCase() === "shop";
const linearProgress = '<div class="linear-activity"><div class="indeterminate"></div></div>';
let isLoading = false;

window.onpopstate = (e) => {
  if (!e.state) return;
  categoryChangeHandler(e.state);
};

const shopPaginationHandler = (e: Event, isClickable?: boolean) => {
  if (!isShopPage || !isClickable) return;
  e.preventDefault();

  const url = new URL((e.target as Element).getAttribute("href"), window.location.origin);
  updatePage(url, false, true);
  if (!window.history.state) window.history.replaceState(window.location.pathname, "", window.location.pathname + window.location.search);
  window.history.pushState(url.href, "", url);
};

const shopRecordsCountHandler = (e: Event) => {
  if (!isShopPage) return;
  e.preventDefault();
  const count = (e.target as Element).innerHTML;

  const url = new URL(window.location.href);
  url.searchParams.set("pageSize", count);
  url.searchParams.set("pageNumber", "1");
  updatePage(url, false, true);
  if (!window.history.state) window.history.replaceState(window.location.pathname, "", window.location.pathname + window.location.search);
  window.history.pushState(url.href, "", url);
};

const attachToPagination = () =>
  $(".pagination li").each((_, el) => {
    el.onclick = (event) => shopPaginationHandler(event, !["active", "disabled"].some((v) => el.classList.contains(v)));
  });

const attachToRecordsCount = () =>
  $("#shop-records-count a").each((_, el) => {
    el.onclick = (e) => shopRecordsCountHandler(e);
  });

if (isShopPage) {
  attachToPagination(), attachToRecordsCount();
}

/**
 *
 * @param url
 * @param handler Which handler method to use?
 * @param firstPage Should try loading first page?
 * @param hasQuery If url already has query string attached on it?
 * @returns
 */
const getShopHandler = async (url: URL, handler: "products" | "counts" | "pagination", firstPage?: boolean, hasQuery?: boolean) => {
  if (!isShopPage) return;

  if (!hasQuery)
    new URLSearchParams(window.location.search).forEach((value, key) => {
      url.searchParams.set(key, value);
    });

  url.searchParams.set("handler", handler);
  if (firstPage) url.searchParams.set("pageNumber", "1");
  url.searchParams.delete("search");
  return await $.get(url.href);
};

const updateFilterLink = () => {
  const filterForm = $("#shop-widget-category form");
  filterForm.attr("action", (_, value) => `${window.location.pathname}?${value.split("?")[1]}`);
  filterForm.find("input[name='search']").removeAttr("value");
};

/**
 *
 * @param e
 * @returns
 */
const categoryChangeHandler = async (e: Event | string) => {
  if (!isShopPage) return;
  const isState = typeof e === "string";
  if (!isState) e.preventDefault();
  if (isLoading) return;
  isLoading = true;

  $("#searchBox").val("");

  const url = new URL(!isState ? (e.target as Element).getAttribute("href") : e, window.location.origin);
  await updatePage(url, !isState).catch((err) => {
    console.log(err);
    $(".linear-activity").remove();
    isLoading = false;
  });
  const currentPageUrl = new URL(window.location.pathname + window.location.search, window.location.origin);
  currentPageUrl.searchParams.delete("search");
  if (!window.history.state) {
    window.history.replaceState(currentPageUrl.pathname, "", currentPageUrl.pathname + currentPageUrl.search);
  }
  if (!isState) {
    const searchParams = new URLSearchParams(window.location.search);
    searchParams.set("pageNumber", "1");
    searchParams.delete("search");
    window.history.pushState(url.pathname, "", url.pathname + "?" + searchParams.toString());
  }
  updateFilterLink();
  isLoading = false;
};

/**
 *
 * @param url Base URL or URL with query.
 * @param firstPage Should try loading first page?
 * @param noSearch Should use window.history.search?
 */
const updatePage = async (url: URL, firstPage?: boolean, noSearch?: boolean) => {
  const container = $("#productsContainer");
  const currentItems = $(".category-item-selected");
  const copyOfUrl = new URL(url);

  container.before(linearProgress);
  const [shopList, shopCount, shopPagination] = await Promise.all([
    getShopHandler(copyOfUrl, "products", firstPage, noSearch),
    getShopHandler(copyOfUrl, "counts", firstPage, noSearch),
    getShopHandler(copyOfUrl, "pagination", firstPage, noSearch),
  ]);

  container.prev().remove();
  $(".shop-part").get(0).scrollIntoView();
  container.replaceWith(shopList);
  $("#shop-result-count").replaceWith(shopCount);
  $(".pagination").html(shopPagination);

  currentItems.removeClass();
  const baseUrl = url.pathname;
  $(`#shop-widget-category a[href='${baseUrl}'], #category-layout a[href='${baseUrl}'], #category-nav a[href='${baseUrl}']`).addClass("category-item-selected");
  attachToPagination();
};

(window as any).categoryChangeHandler = categoryChangeHandler;
