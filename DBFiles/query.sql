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

create function Login_User(_username character varying, _password character varying)
RETURNS int
AS $$
begin
    if(select count() from utilizador u where u.username = _username and u.password = _password) then
        return (select u.iduser from utilizador u where u.username = _username);
    else
        if(select count() from utilizador u where u.username = _username) then
            return -1; -- password is incorrect
        end if;
        return -2; --username is incorrect
    end if;
end
$$
language plpgsql;

select * from consulta
select * from consulta_info(1)

create or replace function consulta_info(_id int)
returns table
(
	_dataconsulta date,
	_descricao character varying,
	_estado int,
	_idtipoconvencao int,
	_pessoa_idutente int,
	_pessoa_idprofsaude int,
	_tipoconsulta_idtipo int,
	_local_idlocal int
)as
$$
begin
	return query
	select dataconsulta,descricao,estado,idtipoconvencao,pessoa_idutente,pessoa_idprofsaude,tipoconsulta_idtipo,local_idlocal from Consulta where idconsulta=_id order by idconsulta;
end
$$
language plpgsql




create or replace function consultas(_id_pessoa int)
returns table
(
	_dataconsulta date,
	_descricao character varying,
	_estado int,
	_tipoconvencao character varying,
	_pessoa_utente character varying,
	_pessoa_profsaude character varying,
	_tipoconsulta_tipo character varying,
	_local_local character varying
)as
$$
begin
	return query
	select c.dataconsulta,c.descricao,c.estado,con.tipo
	from tipoconvencao con inner join consulta c on (con.idtipoconv=c.idtipoconvencao)where pessoa_idutente=_id_pessoa
	select p.nome,p.nome 
	from pessoa p inner join consulta c on (p.idpessoa=c.pessoa_idutente and p.idpessoa=c.pessoa_idprofsaude) where pessoa_idutente=_id_pessoa
	select t.tipo 
	from tipoconsulta t inner join consulta c on (t.idtipo = c.tipoconsulta_idtipo) where pessoa_idutente=_id_pessoa
	select l.morada
	from local l inner join consulta c on (l.idlocal=c.local_idlocal) where pessoa_idutente=_id_pessoa order by idconsulta;
end
$$
language plpgsql


create or replace function consultas(_id_pessoa int)
returns table
(
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
	select c.dataconsulta,c.descricao,c.estado,con.tipo,p.nome,t.tipo,l.morada
	from consulta c LEFT OUTER JOIN  tipoconvencao con on (c.idtipoconvencao=con.idtipoconv)
					LEFT OUTER JOIN pessoa p on (c.pessoa_idprofsaude=p.idpessoa)
					LEFT OUTER JOIN tipoconsulta t on (t.idtipo = c.tipoconsulta_idtipo)
					LEFT OUTER JOIN local l on (l.idlocal=c.local_idlocal) 
					where c.pessoa_idutente=_id_pessoa
					order by idconsulta;
end
$$
language plpgsql




call marca_consulta ('01-02-2021','Tentativa',1,9,5,1,1,'29-12-2020','29-12-2020')
create or replace procedure Marca_Consulta(_dataconsulta timestamp without time zone,_descricao character varying,_idtipoconvencao int,_pessoa_idutente int,_pessoa_idprofsaude int,_tipoconsulta_idtipo int,_local_idlocal int,_datamarcacao timestamp without time zone,_hora timestamp without time zone )
Language 'plpgsql'
AS $$
Declare 
    aux_idconsulta int := nextval('consulta_idconsulta_seq'::regclass);
	_local character varying := (select morada from local where idlocal=_local_idlocal);
Begin

Insert Into consulta(idconsulta, dataconsulta, descricao, estado,idtipoconvencao,pessoa_idutente ,pessoa_idprofsaude,tipoconsulta_idtipo,local_idlocal)
                values (aux_idconsulta,_dataconsulta,_descricao,1,_idtipoconvencao,_pessoa_idutente,_pessoa_idprofsaude,_tipoconsulta_idtipo,_local_idlocal);
Insert Into agenda(utilizador_iduser, data, hora, local, idconsulta)
                values (_pessoa_idutente, _dataconsulta, _hora,_local ,aux_idconsulta);
Insert Into agenda(utilizador_iduser, data, hora, local, idconsulta)
                values (pessoa_idprofsaude, _dataconsulta, _hora,_local ,aux_idconsulta);
Insert Into consultalogs(idpessoa,logdata,newestado,consulta_idconsulta)
				values (_pessoa_idutente,_datamarcacao,1,aux_idconsulta);
Commit;

End;
$$;

select * from pessoa
select * from utilizador
select * from consulta
select * from agenda
select * from local
select * from role
select * from userroles

select * from consultalogs

Call Insert_Utente('pedrogomes0008','José Pedro Gomes da Silva', 253372607,
				   'pedrogomes0008@@gmail.com', 'Trofa', '918860913', '08-05-1994 00:00:00', '123');
select * from utilizador
select * from pessoa 

Call Insert_Utente('alex','Oleksandr',99988877,'oleksandrsierov@gmail.com','Rua Tristeza','123456789','08-02-2020','alex123')