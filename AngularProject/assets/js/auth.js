let formAuth = {
    username: document.getElementById('username'),
    senha: document.getElementById('password')
}

let formSignUp = {
    username: document.getElementById('md-username'),
    email: document.getElementById('md-email'),
    senha: document.getElementById('md-senha')
}

let saveUser = (dataUser) => localStorage.setItem('user', JSON.stringify(dataUser))
let loadUser = () => JSON.parse(localStorage.getItem('user'))
let getUser = (username, password) => {

    return axios({
        method: 'get',
        url: 'http://localhost:49945/api/usuario/teste',
        data: {
            username: username,
            password: password
        }
    })
}

getUser('jonathan', '1234').then(result => console.log(result).catch(error => console.log(error)))

let saveToken = (dataAuth) => localStorage.setItem('auth', dataAuth)
let loadToken = () => JSON.parse(localStorage.getItem('auth'))
let logoff = () => {
    localStorage.removeItem('auth')
    localStorage.removeItem('user')
    redirect('http://localhost:50410/login.html')
}
let redirect = (url) => {
    if (document.location.href === url) return;
    window.location = url
}

if (loadToken()) redirect('http://localhost:50410/')

if (document.querySelector('#btn-entrar')) {
    document.querySelector('#btn-entrar').addEventListener('click', function (event) {
        event.preventDefault()

        let formAuthResponseValidateForm = ''

        if (formAuth.username.value === '' || formAuth.username.value === null) {
            formAuthResponseValidateForm += 'Por favor informe seu usuário. \n'
        }

        if (formAuth.senha.value === '' || formAuth.senha.value === null) {
            formAuthResponseValidateForm += 'Por favor informe sua senha. \n'
        }

        if (formAuthResponseValidateForm !== '') {
            console.log('formAuthResponseValidateForm')
            alert(formAuthResponseValidateForm)
            return
        }

        getUser()
         
        var xhr = new XMLHttpRequest();

        xhr.addEventListener("readystatechange", function () {
            if (this.readyState === 4) {

                if (xhr.status === 200) {
                    saveToken(this.responseText)
                    redirect('http://localhost:50410/')
                } else {
                    alert('Usuário ou senha inválidos, por favor verificar!')
                }
            }
        });

        xhr.open("POST", "http://localhost:49945/token")
        xhr.setRequestHeader("content-type", "application/x-www-form-urlencoded")
        xhr.send(`grant_type=password&Username=${formAuth.username.value}&Password=${formAuth.senha.value}`)

    })
}

if (document.querySelector('#md-cadastro')) {
    document.querySelector('#md-cadastro').addEventListener('click', function (event) {
        event.preventDefault()

        let formSignUpResponseValidateForm = ''

        if (formSignUp.username.value === '' || formSignUp.username.value === null) {
            formSignUpResponseValidateForm += 'Por favor informe o usuário. \n'
        }

        if (formSignUp.email.value === '' || formSignUp.email.value === null) {
            formSignUpResponseValidateForm += 'Por favor informe o email. \n'
        }

        if (formSignUp.senha.value === '' || formSignUp.senha.value === null) {
            formSignUpResponseValidateForm += 'Por favor informe a senha. \n'
        }

        if (formSignUpResponseValidateForm === '') {

            event.preventDefault()

            axios.post(`http://localhost:49945/api/usuario`,
                {
                   'UserName': formSignUp.username.value,
                   'Email': formSignUp.email.value,
                   'Password': formSignUp.senha.value
                })
                .then(resultado => {
                    alert('Usuario criado com sucesso');

                    formAuth.username.value = formSignUp.username.value;
                    formSignUp.username.value = '';
                    formSignUp.email.value = '';
                    formSignUp.senha.value = '';

                })
                .catch(error => console.log(error))


        } else {
            alert(formSignUpResponseValidateForm)
        }

    })
}
