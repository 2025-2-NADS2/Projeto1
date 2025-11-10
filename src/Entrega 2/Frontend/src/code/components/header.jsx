// header.jsx
import React, { useState, useEffect } from "react";
import imgAlma_header from "../../assets/alma_header.png";
import iconHeart from "../../assets/img_heart.png";
import img_profileHeader from "../../assets/img_profileHeader.png";
import "../../style/header.css"; 

export default function Header({ 
  onLoginClick, 
  onHomeClick, 
  onDonateClick, 
  onProfileClick, 
  onSobreClick, 
  onAtividadesClick, 
  onEventosClick, 
  onOuvidoriaClick, 
  onAdminClick, 
  onVoluntarieClick
}) {
  const [menuOpen, setMenuOpen] = useState(false);
  const [isAdmin, setIsAdmin] = useState(false);

  // Função para centralizar clique em links e fechar menu
  const handleLinkClick = (callback) => {
    setMenuOpen(false);
    if (callback) callback();
  };

  // 🔑 Verifica se o usuário é administrador
  useEffect(() => {
    const checkAdmin = async () => {
      const token = localStorage.getItem("token");
      if (!token) return;

      try {
        const res = await fetch("http://localhost:5000/api/Usuario/me", {
          headers: { Authorization: `Bearer ${token}` }
        });
        if (!res.ok) throw new Error("Erro ao buscar usuário");

        const user = await res.json();
        if (user.permissoes.toLowerCase() === "admin") setIsAdmin(true);
      } catch (err) {
        console.error(err);
      }
    };

    checkAdmin();
  }, []);

  // Links principais do menu
  const links = [
    { text: "SOBRE NÓS", onClick: onSobreClick },
    { text: "TRABALHE CONOSCO", onClick: onVoluntarieClick },
    { text: "EVENTOS", onClick: onEventosClick },
    { text: "NOSSAS ATIVIDADES", onClick: onAtividadesClick },
    { text: "OUVIDORIA", onClick: onOuvidoriaClick },
  ];

  // Ícone de coração usado no botão DOAR
  const HeartIcon = () => <img src={iconHeart} alt="ícone de doação" className="icon-heart" />;

  // Decide se deve abrir o perfil ou o login
  const handleProfileAccess = () => {
    const token = localStorage.getItem("token");
    if (token) handleLinkClick(onProfileClick);
    else handleLinkClick(onLoginClick);
  };

  return (
    <header className="header">
      <div className="container">

        {/* Logo / Home */}
        <img
          className="imgAlma_header"
          src={imgAlma_header}
          onClick={() => handleLinkClick(onHomeClick)}
          alt="ALMA"
          style={{ cursor: "pointer" }}
        />

        {/* Menu principal */}
        <nav className={`main-nav ${menuOpen ? "open" : ""}`}>
          <ul>
            {links.map((link, i) => (
              <li key={i}>
                <a 
                  href="#" 
                  onClick={e => { e.preventDefault(); handleLinkClick(link.onClick); }}
                >
                  {link.text}
                </a>
              </li>
            ))}

            {/* Perfil Mobile */}
            <li className="profile-link-mobile">
              <a href="#" onClick={e => { e.preventDefault(); handleProfileAccess(); }}>
                PERFIL
              </a>
            </li>

            {/* Botão Admin Mobile */}
            {isAdmin && (
              <li className="admin-link-mobile">
                <button
                  className="btn-admin"
                  onClick={() => handleLinkClick(onAdminClick)}
                  style={{ fontWeight: "bold", color: "red" }}
                >
                  ADMIN
                </button>
              </li>
            )}

            {/* Botões Mobile */}
            <div className="header-buttons mobile-buttons">
              <button className="btn-primary" onClick={() => handleLinkClick(onDonateClick)}>
                <HeartIcon /> DOAR AGORA
              </button>
              <button className="btn-secondary" onClick={() => handleLinkClick(onLoginClick)}>
                ENTRAR
              </button>
            </div>
          </ul>
        </nav>

        {/* Botões Desktop */}
        <div className="header-buttons desktop-buttons">
          <button className="btn-primary" onClick={onDonateClick}>
            <HeartIcon /> DOAR AGORA
          </button>
          <button className="btn-secondary" onClick={onLoginClick}>
            ENTRAR
          </button>
        </div>

        {/* Perfil Desktop */}
        <img
          className="profile-icon desktop-profile-icon"
          src={img_profileHeader}
          onClick={handleProfileAccess}
          alt="Profile"
          style={{ cursor: "pointer" }}
        />

        {/* Toggle do menu Mobile */}
        <button
          className={`menu-toggle ${menuOpen ? "open" : ""}`}
          onClick={() => setMenuOpen(!menuOpen)}
          aria-label={menuOpen ? "Fechar menu" : "Abrir menu"}
        >
          <span></span><span></span><span></span>
        </button>
      </div>
    </header>
  );
}
