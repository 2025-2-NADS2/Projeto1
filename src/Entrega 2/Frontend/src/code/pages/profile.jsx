import React, { useEffect, useState } from "react";
import Overlay from "../components/overlay.jsx";
import "../../style/profile.css";

export default function Profile() {
  // Estado do usuário
  const [user, setUser] = useState({
    nome: "",
    email: "",
  });

  // Carrega os dados do usuário do localStorage ao montar o componente
  useEffect(() => {
    const storedUser = JSON.parse(localStorage.getItem("userData"));
    if (storedUser) {
      setUser({
        nome: storedUser.nome,
        email: storedUser.email,
      });
    }
  }, []);

  // Função de logout: remove token e dados do usuário
  const handleLogout = () => {
    localStorage.removeItem("token");
    localStorage.removeItem("userData");
    window.location.href = "/";
  };

  return (
    <div className="profile-page">
      {/* Overlay do topo da página */}
      <Overlay />

      <div className="profile-container">
        {/* Cabeçalho do perfil */}
        <div className="profile-header">
          <div className="profile-avatar">
            <img
              src="https://cdn-icons-png.flaticon.com/512/847/847969.png"
              alt="Avatar"
            />
          </div>
          <div className="profile-info">
            <h2>{user.nome || "Usuário"}</h2>
            <p>{user.email || "seuemail@exemplo.com"}</p>
          </div>
        </div>

        {/* Corpo do perfil */}
        <div className="profile-body">
          <h3>Informações da Conta</h3>
          <p>Nome: {user.nome || "—"}</p>
          <p>Email: {user.email || "—"}</p>

          {/* Ações do perfil */}
          <div className="profile-actions">
            <button className="logout-btn" onClick={handleLogout}>
              Sair da Conta
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}
