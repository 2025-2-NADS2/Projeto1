import { react } from "react";

import imgAlma_header from "../assets/alma_header.png";

export default function Header() {
    return (
      <header className="header">
        <div className="container">
          <img className="imgAlma_header" src={imgAlma_header} alt="ALMA" />
          <nav>
            <ul>
              <li><a href="#">SOBRE NÓS</a></li>
              <li><a href="#">TRABALHE CONOSCO</a></li>
              <li><a href="#">EVENTOS</a></li>
              <li><a href="#">CAMPANHAS</a></li>
              <li><a href="#">CAMPANHAS</a></li>              
            </ul>
          </nav>
          <div className="header-buttons">
            <button className="btn-primary">DOAR AGORA</button>
            <button className="btn-secondary">entrar</button>
          </div>
        </div>
      </header>
    );
  }

  