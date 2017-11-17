let elementos = {
    elValorVenda: document.querySelector('#valorVenda'),
    elExchangeVenda: document.querySelector('#exchangeVenda'),
    elValorCompra: document.querySelector('#valorCompra'),
    elExchangeCompra: document.querySelector('#exchangeCompra'),
    elMontante: document.querySelector('#montante'),
    elResultadoQuantidade: document.querySelector('#resultadoQuantidade'),
    elResultadoPorcentagem: document.querySelector('#resultadoPorcentagem')
}

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
        })
        .then(resultado => {
            let data = JSON.parse(`[${resultado}]`)
            renderTable(data)
            
        })
        .catch(error => console.log(error))

}

document.querySelector('.form').addEventListener('submit', function (event) {

    event.preventDefault();

    axios.post(`http://localhost:49945/api/calculator?traderCompra=${elementos.elExchangeCompra.value}&traderVenda=${elementos.elExchangeVenda.value}`,
        {
            'ValorCompra': elementos.elValorCompra.value,
            'ValorVenda': elementos.elValorVenda.value,
            'Montante': elementos.elMontante.value
        })
        .then(resultado => {
            elementos.elResultadoPorcentagem.value = resultado.data.PorcentagemLucro
            elementos.elResultadoQuantidade.value = resultado.data.ValorTotal
        })
        .catch(error => console.log(error))

})

let renderTable = (data) => {
    console.log(data)
    return `
            <table class="table" id="dados-compra">
                <thead>
                    <tr>
                        <th scope="col">Valor Compra</th>
                        <th scope="col">Exchange Compra</th>
                        <th scope="col">Valor Venda</th>
                        <th scope="col">Exchange Venda</th>
                        <th scope="col">Montante</th>
                        <th scope="col">Quantidade</th>
                        <th scope="col">Porcentagem</th>
                        <th scope="col">Ações</th>
                    </tr>
                </thead>
                <tbody>
                    ${data}        
                </tbody>
            </table>
    `;

}

let templateRow = (valorCompra, valorVenda, montante, valorTotal, porcentagemLucro, traderCompra, traderVenda) => {
    return `
        <tr>
            <th scope="row">${valorCompra}</th>
            <td>${traderCompra}</td>
            <td>${valorVenda}</td>
            <td>${traderVenda}</td>
            <td>${montante}</td>
            <td>${valorTotal}</td>
            <td>${porcentagemLucro}</td>
            <td><button type="button" class="btn btn-danger">Excluir</button></td>
        </tr>
    `;
} 