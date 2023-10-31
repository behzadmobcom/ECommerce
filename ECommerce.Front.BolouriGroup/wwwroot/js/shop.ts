const isShopPage = window.location.pathname.split("/")[1].toLowerCase() === "shop";
const linearProgress = '<div class="linear-activity"><div class="indeterminate"></div></div>';

window.onpopstate = (e) => {
  if (!e.state) return;
  categoryChangeHandler(e.state);
};

const shopPaginationHandler = (e: Event, isClickable?: boolean) => {
  if (!isShopPage || !isClickable) return;
  e.preventDefault();

  const url = (e.target as Element).getAttribute("href");
  updatePage(url, false, true);
  if (!window.history.state) window.history.replaceState(window.location.pathname, "", window.location.pathname + window.location.search);
  window.history.pushState(url, "", url);
};

const shopRecordsCountHandler = (e: Event) => {
  if (!isShopPage) return;
  e.preventDefault();
  const count = (e.target as Element).innerHTML;

  const search = window.location.search === "" ? "?pageSize=20" : window.location.search;
  const url = (window.location.pathname + search).replace(/pageSize=[0-9]+/g, `pageSize=${count}`).replace(/pageNumber=[0-9]+/g, "pageNumber=1");
  updatePage(url, false, true);
  if (!window.history.state) window.history.replaceState(window.location.pathname, "", window.location.pathname + window.location.search);
  window.history.pushState(url, "", url);
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
 * @param base Base URL or complete URL with query.
 * @param handler Which handler method to use?
 * @param firstPage Should try loading first page?
 * @param hasQuery If url already has query string attached on it?
 * @returns
 */
const getShopHandler = async (base: string, handler: "products" | "counts" | "pagination", firstPage?: boolean, hasQuery?: boolean) => {
  if (!isShopPage) return;
  let search = hasQuery ? "?" + base.split("?")[1] : window.location.search;
  if (search === "") search += `?handler=${handler}`;
  else search += `&handler=${handler}`;
  if (firstPage) search = search.replace(/pageNumber=[0-9]+/g, "pageNumber=1");
  if (hasQuery) base = base.split("?")[0];
  search = search.replace(/(search=).*?(&|$)/g, "$1$2");
  return await $.get(base + search);
};

const updateFilterLink = () => {
  $("#shop-widget-category form").attr("action", (_, value) => `${window.location.pathname}?${value.split("?")[1]}`);
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

  $("#searchBox").val("");

  const url = !isState ? (e.target as Element).getAttribute("href") : e;
  updatePage(url, !isState);
  if (!window.history.state)
    window.history.replaceState(window.location.pathname, "", window.location.pathname + window.location.search.replace(/(search=).*?(&|$)/g, "$1$2"));
  if (!isState)
    window.history.pushState(url, "", url + window.location.search.replace(/pageNumber=[0-9]+/g, "pageNumber=1").replace(/(search=).*?(&|$)/g, "$1$2"));
  updateFilterLink();
};

/**
 *
 * @param url Base URL or URL with query.
 * @param firstPage Should try loading first page?
 * @param noSearch Should use window.history.search?
 */
const updatePage = async (url: string, firstPage?: boolean, noSearch?: boolean) => {
  const container = $("#productsContainer");
  const currentItems = $(".category-item-selected");

  container.before(linearProgress);
  const [shopList, shopCount, shopPagination] = await Promise.all([
    getShopHandler(url, "products", firstPage, noSearch),
    getShopHandler(url, "counts", firstPage, noSearch),
    getShopHandler(url, "pagination", firstPage, noSearch),
  ]);

  container.prev().remove();
  $(".shop-part").get(0).scrollIntoView();
  container.replaceWith(shopList);
  $("#shop-result-count").replaceWith(shopCount);
  $(".pagination").html(shopPagination);

  currentItems.removeClass();
  const baseUrl = url.split("?")[0];
  $(`#shop-widget-category a[href='${baseUrl}'], #category-layout a[href='${baseUrl}'], #category-nav a[href='${baseUrl}']`).addClass("category-item-selected");
  attachToPagination();
};

(window as any).categoryChangeHandler = categoryChangeHandler;
