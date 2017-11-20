let elementos = {
    elValorVenda: document.querySelector('#valorVenda'),
    elExchangeVenda: document.querySelector('#exchangeVenda'),
    elValorCompra: document.querySelector('#valorCompra'),
    elExchangeCompra: document.querySelector('#exchangeCompra'),
    elMontante: document.querySelector('#montante'),
    elResultadoQuantidade: document.querySelector('#resultadoQuantidade'),
    elResultadoPorcentagem: document.querySelector('#resultadoPorcentagem'),
    elUserName: document.querySelector('#username'),
    elUserPassword: document.querySelector('#password'),
}

let saveToken = (dataAuth) => localStorage.setItem('auth', dataAuth);
let loadToken = () => JSON.parse(localStorage.getItem('auth'));

let getToken = () => {

    var xhr = new XMLHttpRequest();

    xhr.addEventListener("readystatechange", function () {
        if (this.readyState === 4) {
            saveToken(this.responseText);
        }
    });

    xhr.open("POST", "http://localhost:49945/token");
    xhr.setRequestHeader("content-type", "application/x-www-form-urlencoded");

    xhr.send("grant_type=password&Username=joohncruz&Password=123");

}

getToken();

let buscarOrdensDeCompra = () => {
    console.log(loadToken())

    axios.get(`http://localhost:49945/api/buscar`, { headers: { 'Authorization': `bearer ${loadToken().access_token}` } })
        .then(response => document.querySelector('.dados-compra').innerHTML = renderTable(response.data))
        .catch(error => console.log(error))
}

buscarOrdensDeCompra()

let comprar = () => {
    if (elementos.elResultadoQuantidade.value == '' || elementos.elResultadoPorcentagem.value == '') {
        alert('E preciso realizar o calculo')
    } 

    axios.post(`http://localhost:49945/api/comprar`,
        {
            'ValorCompra': elementos.elValorCompra.value,
            'ValorVenda': elementos.elValorVenda.value,
            'Montante': elementos.elMontante.value,
            'ValorTotal': elementos.elResultadoQuantidade.value,
            'PorcentagemLucro': elementos.elResultadoPorcentagem.value,
            'TraderCompra': elementos.elExchangeCompra.value,
            'TraderVenda': elementos.elExchangeVenda.value
        }, { headers: { 'Authorization': `bearer ${loadToken().access_token}` }})
        .then(resultado => console.log(resultado))
        .catch(error => console.log(error))

}

document.querySelector('#formAuth').addEventListener('submit', function (event) {

    event.preventDefault();

    alert(elementos.elUserName.value);
})


document.querySelector('#formCalc').addEventListener('submit', function (event) {

    event.preventDefault();

    axios.post(`http://localhost:49945/api/calculator?traderCompra=${elementos.elExchangeCompra.value}&traderVenda=${elementos.elExchangeVenda.value}`,
        {
            'ValorCompra': elementos.elValorCompra.value,
            'ValorVenda': elementos.elValorVenda.value,
            'Montante': elementos.elMontante.value
        },  { headers: { 'Authorization': `bearer ${loadToken().access_token}` }})
        .then(resultado => {
            elementos.elResultadoPorcentagem.value = resultado.data.PorcentagemLucro
            elementos.elResultadoQuantidade.value = resultado.data.ValorTotal
        })
        .catch(error => console.log(error))

})

let renderTable = (data) => {

    let rows = '';
        
    data.forEach(x => {
        rows += templateRow(x.Id, x.ValorCompra, x.ValorVenda, x.Montante, x.ValorTotal, x.PorcentagemLucro, x.TraderCompra, x.TraderVenda)
    })

    return `
            <table class="table" id="dados-compra">
                <thead>
                    <tr>
                        <th scope="col">Codigo</th>
                        <th scope="col">Valor Compra</th>
                        <th scope="col">Exchange Compra</th>
                        <th scope="col">Valor Venda</th>
                        <th scope="col">Exchange Venda</th>
                        <th scope="col">Montante</th>
                        <th scope="col">Quantidade</th>
                        <th scope="col">Ações</th>
                    </tr>
                </thead>
                <tbody>
                  ${rows}
                </tbody>
            </table>
    `;

}

let templateRow = (id, valorCompra, valorVenda, montante, valorTotal, porcentagemLucro, traderCompra, traderVenda) => {
    return `
        <tr>
            <th scope="row">${id}</th>
            <td>R$ ${valorCompra}</td>
            <td>${traderCompra}</td>
            <td>R$ ${valorVenda}</td>
            <td>${traderVenda}</td>
            <td>R$ ${montante}</td>
            <td>R$ ${valorTotal}</td>
            <td><button type="button" class="btn btn-danger">Excluir</button></td>
        </tr>
    `;
}