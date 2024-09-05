CREATE DATABASE cadastro_veiculos;

USE cadastro_veiculos;

CREATE TABLE Marca (
  id_marca INT AUTO_INCREMENT,
  nome VARCHAR(255),
  PRIMARY KEY (id_marca)
);

CREATE TABLE Modelo (
  id_modelo INT AUTO_INCREMENT,
  descricao VARCHAR(255),
  eixo VARCHAR(255),
  peso VARCHAR(255),
  passageiro VARCHAR(255),
  cavalo VARCHAR(255),
  cilindrada VARCHAR(255),
  fk_marca_id INT,
  PRIMARY KEY (id_modelo),
  FOREIGN KEY (fk_marca_id) REFERENCES Marca(id_marca)
);