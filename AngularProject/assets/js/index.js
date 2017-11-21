if (!loadToken()) redirect('http://localhost:50410/login.html')

let elementos = {
    elValorVenda: document.querySelector('#valorVenda'),
    elExchangeVenda: document.querySelector('#exchangeVenda'),
    elValorCompra: document.querySelector('#valorCompra'),
    elExchangeCompra: document.querySelector('#exchangeCompra'),
    elMontante: document.querySelector('#montante'),
    elResultadoQuantidade: document.querySelector('#resultadoQuantidade'),
    elResultadoPorcentagem: document.querySelector('#resultadoPorcentagem')
}

let exchanges = {
    '1': {
        name: 'FoxBit',
        online: () => { return axios.get('https://api.blinktrade.com/api/v1/BRL/ticker?crypto_currency=BTC') }
    },
    '2': {
        name: 'MecBit',
        online: () => { return axios.get('https://mercadobitcoin.net/api/BTC/ticker/') }
    },
    '3': {
        name: 'B2U',
        online: () => { return axios.get('https://www.bitcointoyou.com/api/ticker.aspx') }
    }
}

let testeFun = (element) => {
    exchanges[element.value].online()
        .then(result => {
            if (exchanges[element.value].name == 'MecBit' || exchanges[element.value].name == 'B2U') {
                elementos.elValorCompra.value = result.data.ticker.last
            }

            if (exchanges[element.value].name == 'FoxBit') {
                alert('Problema de cors origin')
            }
        })
        .catch(error => console.log(error))
}

let buscarOrdensDeCompra = () => {

    axios.get(`http://localhost:49945/api/buscar`, { headers: { 'Authorization': `bearer ${loadToken().access_token}` } })
        .then(response => document.querySelector('.dados-compra').innerHTML = renderTable(response.data))
        .catch(error => console.log(error))

}

buscarOrdensDeCompra()

let comprar = () => {
    if (elementos.elResultadoQuantidade.value === '' || elementos.elResultadoPorcentagem.value === '') {
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
        .then(resultado => {
            elementos.elValorCompra.value == '';
            elementos.elValorVenda.value == '';
            elementos.elMontante.value == '';

            alert('Ordem de compra executada!!')

            buscarOrdensDeCompra()

        })
        .catch(error => console.log(error))

}

document.querySelector('#formCalc').addEventListener('submit', function (event) {

    event.preventDefault()

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

    if (data.length === 0) return ''

    let rows = ''
        
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
    `
}

let templateRow = (id, valorCompra, valorVenda, montante, valorTotal, porcentagemLucro, traderCompra, traderVenda) => {
    return `
        <tr>
            <th scope="row">${id}</th>
            <td>R$ ${valorCompra}</td>
            <td>${exchanges[traderCompra].name}</td>
            <td>R$ ${valorVenda}</td>
            <td>${exchanges[traderVenda].name}</td>
            <td>R$ ${montante}</td>
            <td>R$ ${valorTotal}</td>
            <td><button type="button" class="btn btn-danger">Excluir</button></td>
        </tr>
    `
}