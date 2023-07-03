const price = document.getElementsByClassName('price')

for (var i = 0; i < price.length; i++) {
  if (price[i].innerText && price[i].innerText != 'undefined') {
    const str = price[i].innerText?.toString()?.split(".");
    if (str[0]?.length >= 4) {
      str[0] = str[0]?.replace(/(\d)(?=(\d{3})+$)/g, "$1.");
    }
    if (str[1] && str[1]?.length >= 5) {
      str[1] = str[1]?.replace(/(\d{3})/g, "$1 ");
    }
    document.getElementsByClassName('price')[i].innerHTML=str.join(",")
  }
}

