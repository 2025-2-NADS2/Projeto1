import React, { useState } from "react";
import "../../style/login.css";
import Overlay from "../components/overlay.jsx";
import api from "../../services/api.js";

export default function Login() {
  const [isRegister, setIsRegister] = useState(false);

  // Campos de login
  const [loginData, setLoginData] = useState({ email: "", senha: "" });

  // Campos de registro
  const [registerData, setRegisterData] = useState({
    nome: "",
    email: "",
    telefone: "",
    senha: "",
    confirmarSenha: ""
  });

  const togglePanel = () => setIsRegister(!isRegister);

  // Função de registro
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
      setIsRegister(false);
    } catch (error) {
      console.error("Erro ao registrar:", error);
      alert("Erro ao criar conta.");
    }
  };

  // Função de login
  const handleLogin = async () => {
    try {
      const response = await api.post("/usuario/login", {
        email: loginData.email,
        senha: loginData.senha
      });

      const token = response.data.token;

      // Decodifica o token para pegar email e nome
      const base64Payload = token.split('.')[1];
      const payload = JSON.parse(atob(base64Payload));

      const userData = {
        id: payload.nameid, // Claim NameIdentifier
        nome: payload.Nome, // Claim customizada que você criou
        email: payload.email
      };

      localStorage.setItem("token", token);
      localStorage.setItem("userData", JSON.stringify(userData));

      alert("Login realizado com sucesso!");
      window.location.href = "/profile";
    } catch (error) {
      console.error("Erro no login:", error);
      alert("Email ou senha incorretos.");
    }
  };

  return (
    <div className="login-page">
      <Overlay className="topLogin" />

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

        {/* Painel verde animado */}
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
