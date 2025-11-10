// src/code/pagamento/pix.jsx

// Função utilitária para calcular CRC16 (requisito do Pix)
function crc16(payload) {
    let crc = 0xffff;
    for (let offset = 0; offset < payload.length; offset++) {
      crc ^= payload.charCodeAt(offset) << 8;
      for (let bit = 0; bit < 8; bit++) {
        if ((crc & 0x8000) !== 0) {
          crc = (crc << 1) ^ 0x1021;
        } else {
          crc <<= 1;
        }
        crc &= 0xffff;
      }
    }
    return crc.toString(16).toUpperCase().padStart(4, "0");
  }
  
  // Monta o campo no formato ID + length + valor
  function montarCampo(id, valor) {
    const length = valor.length.toString().padStart(2, "0");
    return `${id}${length}${valor}`;
  }
  
  // Função principal: gera um payload Pix válido
  function gerarPayloadPix(chave, valor, descricao = "", nome = "Instituto Alma", cidade = "SAO PAULO") {
    const campo00 = montarCampo("00", "01");
    const campo01 = montarCampo("01", "12");
  
    const gui = montarCampo("00", "br.gov.bcb.pix");
    const chavePixCampo = montarCampo("01", chave);
    const infoAdicional = descricao ? montarCampo("02", descricao) : "";
    const campo26 = montarCampo("26", gui + chavePixCampo + infoAdicional);
  
    const campo52 = montarCampo("52", "0000");
    const campo53 = montarCampo("53", "986");
    const campo54 = montarCampo("54", valor.toFixed(2));
    const campo58 = montarCampo("58", "BR");
    const campo59 = montarCampo("59", nome.toUpperCase().substring(0, 25));
    const campo60 = montarCampo("60", cidade.toUpperCase().substring(0, 15));
    const txid = montarCampo("05", "***");
    const campo62 = montarCampo("62", txid);
  
    const payloadSemCRC =
      campo00 +
      campo01 +
      campo26 +
      campo52 +
      campo53 +
      campo54 +
      campo58 +
      campo59 +
      campo60 +
      campo62 +
      "6304";
  
    const crc = crc16(payloadSemCRC);
    return payloadSemCRC + crc;
  }
  
  // Função para gerar o QR code URL
  function gerarQRCodePix(payload) {
    return `https://api.qrserver.com/v1/create-qr-code/?data=${encodeURIComponent(payload)}&size=300x300`;
  }
  
  // Função principal exportada
  export default function gerarPix(valor) {
    const chavePix = "victorcamargo0302@gmail.com";
  
    if (!valor || isNaN(valor) || valor <= 0) {
      console.error("Valor inválido para gerar Pix:", valor);
      return null;
    }
  
    const payload = gerarPayloadPix(chavePix, parseFloat(valor));
    const qrCodeUrl = gerarQRCodePix(payload);
  
    return {
      valor: parseFloat(valor).toFixed(2),
      qrCodeUrl,
      payload,
    };
  }
  