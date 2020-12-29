create or replace procedure Insert_Utente(_user character varying,_name character varying,_nif numeric,_email character varying,_morada character varying, _tele character varying,_data timestamp without time zone, _pass character varying)
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
Insert into userroles(role_idrole, utilizador_iduser) values (1, aux_idUser);
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


create or replace function consulta_info(_id int)
returns table
(
	_dataconsulta timestamp without time zone,
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




Call Insert_Utente('pedrogomes0008','José Pedro Gomes da Silva', 253372607,
				   'pedrogomes0008@@gmail.com', 'Trofa', '918860913', '08-05-1994 00:00:00', '123');
select * from utilizador
select * from pessoa 

Call Insert_Utente('alex','Oleksandr',99988877,'oleksandrsierov@gmail.com','Rua Tristeza','123456789','08-02-2020','alex123')