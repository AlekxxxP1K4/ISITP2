select * from consulta
select * from utilizador
select * from Pessoa
select * from local
select * from tipoconsulta
select * from userroles
select * from role

select ur.utilizador_iduser,p1.nome from userroles ur inner join pessoa p1 on (ur.utilizador_iduser=p1.idpessoa) where role_idrole=2
Call Insert_Utente('ricardo11','Ricardo Alfredo da Silva', 243372621,
				   'ricardoalfredo@gmail.com', 'Barcelos', '919889913', '08-05-1984 00:00:00', 'ric84',2);
				   
create or replace procedure Insert_Utente(_user character varying,_name character varying,_nif numeric,_email character varying,_morada character varying, _tele character varying,_data timestamp without time zone, _pass character varying,_role int)
Language 'plpgsql'
AS $$
Declare 
    aux_idUser int := nextval('utilizador_iduser_seq'::regclass);
    aux_idPessoa int := nextval('pessoa_idpessoa_seq'::regclass);
Begin

Insert Into pessoa(idpessoa, nome, telefone, nif, morada, datanascimento, pessoa_idresponsavel)
                values (aux_idPessoa, _name, _tele, _nif, _morada, _data,NULL);
Insert Into utilizador(iduser, username, password, email, estado, estadosessao, pessoa_idpessoa)
                values (aux_idUser, _user, _pass, _email, 'ativo', FALSE , aux_idPessoa);
Insert into userroles(role_idrole, utilizador_iduser) values (_role, aux_idUser);
Commit;

End;
$$;

call marca_consulta ('01-02-2021','Tentativa asd',1,10,5,1,1,'29-12-2020','29-12-2020')

create or replace procedure Marca_Consulta(_dataconsulta timestamp without time zone,_descricao character varying,_idtipoconvencao int,_pessoa_idutente int,_pessoa_idprofsaude int,_tipoconsulta_idtipo int,_datamarcacao timestamp without time zone,_hora timestamp without time zone )
Language 'plpgsql'
AS $$
Declare 
    aux_idconsulta int := nextval('consulta_idconsulta_seq'::regclass);
	aux_idlocal int := nextval('local_idlocal_seq'::regclass);
	_local character varying := (select morada from pessoa where idpessoa=_pessoa_idutente);
	_contacto character varying :=(select telefone from pessoa where idpessoa=_pessoa_idutente);
Begin
Insert Into local(idlocal,morada,contacto,estado)
                values (aux_idlocal,_local,_contacto,TRUE);
Insert Into consulta(idconsulta, dataconsulta, descricao, estado,idtipoconvencao,pessoa_idutente ,pessoa_idprofsaude,tipoconsulta_idtipo,local_idlocal)
                values (aux_idconsulta,_dataconsulta,_descricao,1,_idtipoconvencao,_pessoa_idutente,_pessoa_idprofsaude,_tipoconsulta_idtipo,aux_idlocal);
Insert Into agenda(utilizador_iduser, data, hora, local, idconsulta)
                values (_pessoa_idutente, _dataconsulta, _hora,_local ,aux_idconsulta);
Insert Into agenda(utilizador_iduser, data, hora, local, idconsulta)
                values (_pessoa_idprofsaude, _dataconsulta, _hora,_local ,aux_idconsulta);
Insert Into consultalogs(idpessoa,logdata,newestado,consulta_idconsulta)
				values (_pessoa_idutente,_datamarcacao,1,aux_idconsulta);
Commit;

End;
$$;