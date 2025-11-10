// index.jsx (Root)
import React, { useState, useEffect } from "react";
import ReactDOM from "react-dom/client";

import Header from "./code/components/header.jsx";
import Footer from "./code/components/footer.jsx";
import App from "./code/pages/index.jsx";
import Login from "./code/pages/login.jsx";
import Donate from "./code/pages/donate.jsx";
import Profile from "./code/pages/profile.jsx";
import Sobre from "./code/pages/sobre.jsx";
import Atividades from "./code/pages/atividades.jsx";
import DonationPortal from "./code/pages/donatePortal.jsx"; 
import Eventos from "./code/pages/eventos.jsx"; 
import Ouvidoria from "./code/pages/ouvidoria.jsx"; 
import Transparencia from "./code/pages/transparencia.jsx"; 
import AdminPanel from "./code/pages/admin.jsx";
import Voluntariado from "./code/pages/voluntariado.jsx";

function Root() {
  const [currentPage, setCurrentPage] = useState(
    localStorage.getItem("currentPage") || "app"
  );

  useEffect(() => {
    localStorage.setItem("currentPage", currentPage);
  }, [currentPage]);

  const renderPage = () => {
    switch (currentPage) {
      case "app":
        return <App />;
      case "login":
        return <Login onBack={() => setCurrentPage("app")} />;
      case "donate":
        return (
          <Donate
            onBack={() => setCurrentPage("app")}
            onDonatePortal={() => setCurrentPage("donatePortal")}
          />
        );
      case "profile":
        return <Profile onBack={() => setCurrentPage("app")} />;
      case "sobre":
        return <Sobre onBack={() => setCurrentPage("app")} />;
      case "atividades":
        return <Atividades onBack={() => setCurrentPage("app")} />;
      case "eventos":
        return <Eventos onBack={() => setCurrentPage("app")} />;
      case "donatePortal":
        return (
          <DonationPortal
            onBack={() => setCurrentPage("donate")}
            onLoginClick={() => setCurrentPage("login")}
          />
        );
      case "ouvidoria": 
        return <Ouvidoria onBack={() => setCurrentPage("app")} />;
      case "transparencia":
        return <Transparencia onBack={() => setCurrentPage("app")} />;
      case "admin":
        return <AdminPanel onBack={() => setCurrentPage("app")} />; 
      case "voluntariado":
        return <Voluntariado onBack={() => setCurrentPage("app")} />; 
      default:
        return <App />;
    }
  };

  return (
    <>
      <Header
        onLoginClick={() => setCurrentPage("login")}
        onHomeClick={() => setCurrentPage("app")}
        onDonateClick={() => setCurrentPage("donate")}
        onProfileClick={() => setCurrentPage("profile")}
        onSobreClick={() => setCurrentPage("sobre")}
        onAtividadesClick={() => setCurrentPage("atividades")}
        onEventosClick={() => setCurrentPage("eventos")}
        onOuvidoriaClick={() => setCurrentPage("ouvidoria")} 
        onAdminClick={() => setCurrentPage("admin")}
        onVoluntarieClick={() => setCurrentPage("voluntariado")}
      />
      {renderPage()}
      <Footer 
        onEventosClick={() => setCurrentPage("eventos")}
        onAtividadesClick={() => setCurrentPage("atividades")}
        onSobreClick={() => setCurrentPage("sobre")}
        onDonateClick={() => setCurrentPage("donate")}
        onTransparenciaClick={() => setCurrentPage("transparencia")}
      />
    </>
  );
}

const root = ReactDOM.createRoot(document.getElementById("root"));
root.render(<Root />);
