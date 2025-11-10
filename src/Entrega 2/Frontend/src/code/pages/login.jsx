import React, { useState } from "react";
import "../../style/login.css";
import Overlay from "../components/overlay.jsx";
import api from "../../services/api.js";

export default function Login() {
  // Estado que controla se o painel de registro está ativo
  const [isRegister, setIsRegister] = useState(false);

  // Dados do formulário de login
  const [loginData, setLoginData] = useState({ email: "", senha: "" });

  // Dados do formulário de registro
  const [registerData, setRegisterData] = useState({
    nome: "",
    email: "",
    telefone: "",
    senha: "",
    confirmarSenha: ""
  });

  // Alterna entre Login e Registro
  const togglePanel = () => setIsRegister(!isRegister);

  // Função para registrar usuário
  const handleRegister = async () => {
    try {
      const body = {
        nome: registerData.nome,
        email: registerData.email,
        telefone: registerData.telefone,
        senha: registerData.senha,
        confirmarSenha: registerData.confirmarSenha,
        status: "1",
        permissoes: "Default"
      };

      const response = await api.post("/usuario/post/cadastro/usuario", body);
      alert("Conta criada com sucesso! Faça login.");
      console.log("✅ Cadastro:", response.data);

      // Volta para o painel de login após registro
      setIsRegister(false);
    } catch (error) {
      console.error("Erro ao registrar:", error);
      alert("Erro ao criar conta.");
    }
  };

  // Função para login de usuário
  const handleLogin = async () => {
    try {
      const response = await api.post("/usuario/login", {
        email: loginData.email,
        senha: loginData.senha
      });

      const token = response.data.token;

      // Decodifica o token para pegar informações do usuário
      const base64Payload = token.split('.')[1];
      const payload = JSON.parse(atob(base64Payload));

      const userData = {
        id: payload.nameid,   // Claim NameIdentifier
        nome: payload.Nome,   // Claim personalizada
        email: payload.email
      };

      // Salva token e dados do usuário no localStorage
      localStorage.setItem("token", token);
      localStorage.setItem("userData", JSON.stringify(userData));

      alert("Login realizado com sucesso!");
      window.location.href = "/profile"; // Redireciona para o perfil
    } catch (error) {
      console.error("Erro no login:", error);
      alert("Email ou senha incorretos.");
    }
  };

  return (
    <div className="login-page">
      {/* Overlay superior decorativo */}
      <Overlay className="topLogin" />

      {/* Container principal do login/registro */}
      <div className={`login-frame ${isRegister ? "show-register" : ""}`}>
        
        {/* Seção de Login */}
        <div className="form-left-side">
          <div className="form-side login-left">
            <h2 className="login-txt">Login</h2>
            <input
              type="text"
              placeholder="Email"
              value={loginData.email}
              onChange={(e) =>
                setLoginData({ ...loginData, email: e.target.value })
              }
            />
            <input
              type="password"
              placeholder="Senha"
              value={loginData.senha}
              onChange={(e) =>
                setLoginData({ ...loginData, senha: e.target.value })
              }
            />
            <a href="#" className="forgot-password">
              Esqueceu a senha?
            </a>
            <button className="btn-login" onClick={handleLogin}>
              ENTRAR
            </button>
          </div>
        </div>

        {/* Seção de Registro */}
        <div className="form-side register-right">
          <h2>Registrar</h2>
          <input
            type="text"
            placeholder="Nome"
            value={registerData.nome}
            onChange={(e) =>
              setRegisterData({ ...registerData, nome: e.target.value })
            }
          />
          <input
            type="text"
            placeholder="Email"
            value={registerData.email}
            onChange={(e) =>
              setRegisterData({ ...registerData, email: e.target.value })
            }
          />
          <input
            type="text"
            placeholder="Telefone"
            value={registerData.telefone}
            onChange={(e) =>
              setRegisterData({ ...registerData, telefone: e.target.value })
            }
          />
          <input
            type="password"
            placeholder="Senha"
            value={registerData.senha}
            onChange={(e) =>
              setRegisterData({ ...registerData, senha: e.target.value })
            }
          />
          <input
            type="password"
            placeholder="Confirmar Senha"
            value={registerData.confirmarSenha}
            onChange={(e) =>
              setRegisterData({
                ...registerData,
                confirmarSenha: e.target.value
              })
            }
          />
          <button className="btn-login" onClick={handleRegister}>
            REGISTRAR
          </button>
        </div>

        {/* Painel verde animado de alternância */}
        <div className="login-right">
          <h2>{isRegister ? "Bem vindo(a) de Volta!" : "Novo por aqui?"}</h2>
          <p>
            {isRegister
              ? "Para acessar sua conta, clique abaixo e volte ao login."
              : "Para se manter conectado, por favor, crie uma conta."}
          </p>
          <button className="btn-register" onClick={togglePanel}>
            {isRegister ? "Entrar" : "Registrar"}
          </button>
        </div>
      </div>
    </div>
  );
}
