select * from consulta
select * from utilizador
select * from Pessoa
select * from local
select * from tipoconsulta
select * from userroles
select * from role
select * from consultas(9)
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

select * from utilizador

select * from user_update(5,'1232','123452')


create or replace function user_update(_iduser int,pw character varying, newpw character varying)
returns int as
$$
begin
	update utilizador
	set
		password=newpw
	where iduser=_iduser and password=pw;
	if found then
		return 1;
	else 
		return 0;
	end if;
end
$$
language plpgsql

select * from consulta_delete(4)

DROP FUNCTION consultas(integer)

create or replace function consultas(_id_pessoa int)
returns table
(
	_idconsulta int,
	_dataconsulta date,
	_descricao character varying,
	_estado int,
	_tipoconvencao character varying,
	_pessoa_profsaude character varying,
	_tipoconsulta_tipo character varying,
	_local_local character varying
)as
$$
begin
	return query
	select c.idconsulta,c.dataconsulta,c.descricao,c.estado,con.tipo,p.nome,t.tipo,l.morada
	from consulta c LEFT OUTER JOIN  tipoconvencao con on (c.idtipoconvencao=con.idtipoconv)
					LEFT OUTER JOIN pessoa p on (c.pessoa_idprofsaude=p.idpessoa)
					LEFT OUTER JOIN tipoconsulta t on (t.idtipo = c.tipoconsulta_idtipo)
					LEFT OUTER JOIN local l on (l.idlocal=c.local_idlocal) 
					where c.pessoa_idutente=_id_pessoa
					order by idconsulta;
end
$$
language plpgsql


create or replace function Consulta_Delete(_idconsulta int)
returns int as
$$
begin
	update consulta
	set
		estado=0
	where idconsulta=_idconsulta;
	if found then
		return 1;
	else 
		return 0;
	end if;
end
$$
language plpgsql