﻿let formAuth = {
    username: document.getElementById('username'),
    senha: document.getElementById('password')
}

let formSignUp = {
    username: document.getElementById('md-username'),
    email: document.getElementById('md-email'),
    senha: document.getElementById('md-senha')
}

let saveToken = (dataAuth) => localStorage.setItem('auth', dataAuth)
let loadToken = () => JSON.parse(localStorage.getItem('auth'))
let logoff = () => {
    localStorage.removeItem('auth')
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

        console.log(formSignUpResponseValidateForm)

        if (formSignUpResponseValidateForm === '') {
            alert('md-cadastro')
        } else {
            alert(formSignUpResponseValidateForm)
        }

    })
}
