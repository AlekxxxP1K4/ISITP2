PGDMP                         x            isitp2    12.2    12.2 t    �           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            �           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            �           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            �           1262    24013    isitp2    DATABASE     �   CREATE DATABASE isitp2 WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Portuguese_Brazil.1252' LC_CTYPE = 'Portuguese_Brazil.1252';
    DROP DATABASE isitp2;
                postgres    false            �            1255    24014    get_roles(integer)    FUNCTION     d  CREATE FUNCTION public.get_roles(_id integer) RETURNS TABLE(_iduser integer, _nomerole character varying)
    LANGUAGE plpgsql
    AS $$
begin
	return query 
		select u.iduser, r.nomerole from utilizador u inner join userroles ur on u.iduser = ur.utilizador_iduser 
											inner join role r on ur.role_idrole = r.idrole
		where u.iduser = _id;
end
$$;
 -   DROP FUNCTION public.get_roles(_id integer);
       public          postgres    false            �            1255    24015 �   insert_utente(character varying, character varying, numeric, character varying, character varying, character varying, timestamp without time zone, character varying) 	   PROCEDURE     n  CREATE PROCEDURE public.insert_utente(_user character varying, _name character varying, _nif numeric, _email character varying, _morada character varying, _tele character varying, _data timestamp without time zone, _pass character varying)
    LANGUAGE plpgsql
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
 �   DROP PROCEDURE public.insert_utente(_user character varying, _name character varying, _nif numeric, _email character varying, _morada character varying, _tele character varying, _data timestamp without time zone, _pass character varying);
       public          postgres    false            �            1255    24016 0   login_user(character varying, character varying)    FUNCTION     �  CREATE FUNCTION public.login_user(_username character varying, _password character varying) RETURNS integer
    LANGUAGE plpgsql
    AS $$
begin
	if(select count(*) from utilizador u where u.username = _username and u.password = _password) then
		return (select u.iduser from utilizador u where u.username = _username);
	else
		if(select count(*) from utilizador u where u.username = _username) then
			return -1; -- password is incorrect
		end if;
		return -2; --username is incorrect
	end if;
end
$$;
 [   DROP FUNCTION public.login_user(_username character varying, _password character varying);
       public          postgres    false            �            1259    24017    agenda    TABLE     �   CREATE TABLE public.agenda (
    utilizador_iduser integer NOT NULL,
    data date NOT NULL,
    hora time(6) without time zone NOT NULL,
    local character varying(255) NOT NULL,
    idconsulta integer NOT NULL
);
    DROP TABLE public.agenda;
       public         heap    postgres    false            �            1259    24020    consulta    TABLE       CREATE TABLE public.consulta (
    idconsulta integer NOT NULL,
    dataconsulta date NOT NULL,
    datamarcacao date NOT NULL,
    descricao character varying(255),
    estado integer NOT NULL,
    idtipoconvencao integer,
    pessoa_idutente integer NOT NULL,
    pessoa_idprofsaude integer NOT NULL,
    tipoconsulta_idtipo integer NOT NULL,
    local_idlocal integer NOT NULL
);
    DROP TABLE public.consulta;
       public         heap    postgres    false            �            1259    24023    consulta_idconsulta_seq    SEQUENCE     �   CREATE SEQUENCE public.consulta_idconsulta_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.consulta_idconsulta_seq;
       public          postgres    false    203            �           0    0    consulta_idconsulta_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.consulta_idconsulta_seq OWNED BY public.consulta.idconsulta;
          public          postgres    false    204            �            1259    24025    consultalogs    TABLE     �   CREATE TABLE public.consultalogs (
    idpessoa integer NOT NULL,
    logdata date NOT NULL,
    prevestado integer,
    newestado integer NOT NULL,
    consulta_idconsulta integer NOT NULL
);
     DROP TABLE public.consultalogs;
       public         heap    postgres    false            �            1259    24028 	   convencao    TABLE     �   CREATE TABLE public.convencao (
    validade date NOT NULL,
    identificacao character varying(255) NOT NULL,
    pessoa_idpessoa integer NOT NULL,
    tipoconvencao_idtipoconv integer NOT NULL
);
    DROP TABLE public.convencao;
       public         heap    postgres    false            �            1259    24031 
   documentos    TABLE     �   CREATE TABLE public.documentos (
    caminhoficheiro character varying(255) NOT NULL,
    fichaclinica_pessoa_idpessoa integer NOT NULL
);
    DROP TABLE public.documentos;
       public         heap    postgres    false            �            1259    24034    fichaclinica    TABLE     z   CREATE TABLE public.fichaclinica (
    descricao character varying(255) NOT NULL,
    pessoa_idpessoa integer NOT NULL
);
     DROP TABLE public.fichaclinica;
       public         heap    postgres    false            �            1259    24037    local    TABLE     �   CREATE TABLE public.local (
    idlocal integer NOT NULL,
    morada character varying(255),
    contacto character varying(255),
    estado boolean NOT NULL,
    nome character varying(255) NOT NULL
);
    DROP TABLE public.local;
       public         heap    postgres    false            �            1259    24043    local_idlocal_seq    SEQUENCE     �   CREATE SEQUENCE public.local_idlocal_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 (   DROP SEQUENCE public.local_idlocal_seq;
       public          postgres    false    209            �           0    0    local_idlocal_seq    SEQUENCE OWNED BY     G   ALTER SEQUENCE public.local_idlocal_seq OWNED BY public.local.idlocal;
          public          postgres    false    210            �            1259    24045 
   permissoes    TABLE     x   CREATE TABLE public.permissoes (
    role_idrole integer NOT NULL,
    tipopermissaoidtipopermissao integer NOT NULL
);
    DROP TABLE public.permissoes;
       public         heap    postgres    false            �            1259    24048    pessoa    TABLE       CREATE TABLE public.pessoa (
    idpessoa integer NOT NULL,
    nome character varying(255) NOT NULL,
    telefone character varying(255),
    nif integer NOT NULL,
    morada character varying(255),
    datanascimento date NOT NULL,
    pessoa_idresponsavel integer
);
    DROP TABLE public.pessoa;
       public         heap    postgres    false            �            1259    24054    pessoa_idpessoa_seq    SEQUENCE     �   CREATE SEQUENCE public.pessoa_idpessoa_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.pessoa_idpessoa_seq;
       public          postgres    false    212            �           0    0    pessoa_idpessoa_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.pessoa_idpessoa_seq OWNED BY public.pessoa.idpessoa;
          public          postgres    false    213            �            1259    24056    recibo    TABLE     �   CREATE TABLE public.recibo (
    idrecibo integer NOT NULL,
    data date NOT NULL,
    tipoconsulta character varying(255),
    preco real NOT NULL,
    estado character varying(255) NOT NULL,
    nif integer,
    consulta_idconsulta integer NOT NULL
);
    DROP TABLE public.recibo;
       public         heap    postgres    false            �            1259    24062    recibo_idrecibo_seq    SEQUENCE     �   CREATE SEQUENCE public.recibo_idrecibo_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 *   DROP SEQUENCE public.recibo_idrecibo_seq;
       public          postgres    false    214            �           0    0    recibo_idrecibo_seq    SEQUENCE OWNED BY     K   ALTER SEQUENCE public.recibo_idrecibo_seq OWNED BY public.recibo.idrecibo;
          public          postgres    false    215            �            1259    24064    role    TABLE     �   CREATE TABLE public.role (
    idrole integer NOT NULL,
    nomerole character varying(255) NOT NULL,
    estado boolean NOT NULL
);
    DROP TABLE public.role;
       public         heap    postgres    false            �            1259    24067    role_idrole_seq    SEQUENCE     �   CREATE SEQUENCE public.role_idrole_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 &   DROP SEQUENCE public.role_idrole_seq;
       public          postgres    false    216            �           0    0    role_idrole_seq    SEQUENCE OWNED BY     C   ALTER SEQUENCE public.role_idrole_seq OWNED BY public.role.idrole;
          public          postgres    false    217            �            1259    24069    tipoconsulta    TABLE     �   CREATE TABLE public.tipoconsulta (
    idtipo integer NOT NULL,
    tipo character varying(255) NOT NULL,
    preco real NOT NULL,
    estado boolean NOT NULL
);
     DROP TABLE public.tipoconsulta;
       public         heap    postgres    false            �            1259    24072    tipoconsulta_idtipo_seq    SEQUENCE     �   CREATE SEQUENCE public.tipoconsulta_idtipo_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 .   DROP SEQUENCE public.tipoconsulta_idtipo_seq;
       public          postgres    false    218            �           0    0    tipoconsulta_idtipo_seq    SEQUENCE OWNED BY     S   ALTER SEQUENCE public.tipoconsulta_idtipo_seq OWNED BY public.tipoconsulta.idtipo;
          public          postgres    false    219            �            1259    24074    tipoconvencao    TABLE     �   CREATE TABLE public.tipoconvencao (
    idtipoconv integer NOT NULL,
    tipo character varying(255) NOT NULL,
    descontoperc numeric(19,0) NOT NULL,
    estado boolean NOT NULL
);
 !   DROP TABLE public.tipoconvencao;
       public         heap    postgres    false            �            1259    24077    tipoconvencao_idtipoconv_seq    SEQUENCE     �   CREATE SEQUENCE public.tipoconvencao_idtipoconv_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 3   DROP SEQUENCE public.tipoconvencao_idtipoconv_seq;
       public          postgres    false    220            �           0    0    tipoconvencao_idtipoconv_seq    SEQUENCE OWNED BY     ]   ALTER SEQUENCE public.tipoconvencao_idtipoconv_seq OWNED BY public.tipoconvencao.idtipoconv;
          public          postgres    false    221            �            1259    24079    tipopermissao    TABLE     �   CREATE TABLE public.tipopermissao (
    idtipopermissao integer NOT NULL,
    textopermissao character varying(255) NOT NULL,
    estado boolean NOT NULL
);
 !   DROP TABLE public.tipopermissao;
       public         heap    postgres    false            �            1259    24082 !   tipopermissao_idtipopermissao_seq    SEQUENCE     �   CREATE SEQUENCE public.tipopermissao_idtipopermissao_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 8   DROP SEQUENCE public.tipopermissao_idtipopermissao_seq;
       public          postgres    false    222            �           0    0 !   tipopermissao_idtipopermissao_seq    SEQUENCE OWNED BY     g   ALTER SEQUENCE public.tipopermissao_idtipopermissao_seq OWNED BY public.tipopermissao.idtipopermissao;
          public          postgres    false    223            �            1259    24084    tipotratamento    TABLE     �   CREATE TABLE public.tipotratamento (
    idtipo integer NOT NULL,
    tipo character varying(255),
    preco real NOT NULL,
    estado boolean NOT NULL
);
 "   DROP TABLE public.tipotratamento;
       public         heap    postgres    false            �            1259    24087    tipotratamento_idtipo_seq    SEQUENCE     �   CREATE SEQUENCE public.tipotratamento_idtipo_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 0   DROP SEQUENCE public.tipotratamento_idtipo_seq;
       public          postgres    false    224            �           0    0    tipotratamento_idtipo_seq    SEQUENCE OWNED BY     W   ALTER SEQUENCE public.tipotratamento_idtipo_seq OWNED BY public.tipotratamento.idtipo;
          public          postgres    false    225            �            1259    24089 
   tratamento    TABLE     �   CREATE TABLE public.tratamento (
    descricao character varying(255),
    preco real,
    tipotratamento_idtipo integer NOT NULL,
    consulta_idconsulta integer NOT NULL
);
    DROP TABLE public.tratamento;
       public         heap    postgres    false            �            1259    24092 	   userroles    TABLE     l   CREATE TABLE public.userroles (
    role_idrole integer NOT NULL,
    utilizador_iduser integer NOT NULL
);
    DROP TABLE public.userroles;
       public         heap    postgres    false            �            1259    24095 
   utilizador    TABLE     <  CREATE TABLE public.utilizador (
    iduser integer NOT NULL,
    username character varying(255) NOT NULL,
    password character varying(255) NOT NULL,
    email character varying(255) NOT NULL,
    estado character varying(255) NOT NULL,
    estadosessao boolean NOT NULL,
    pessoa_idpessoa integer NOT NULL
);
    DROP TABLE public.utilizador;
       public         heap    postgres    false            �            1259    24101    utilizador_iduser_seq    SEQUENCE     �   CREATE SEQUENCE public.utilizador_iduser_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;
 ,   DROP SEQUENCE public.utilizador_iduser_seq;
       public          postgres    false    228            �           0    0    utilizador_iduser_seq    SEQUENCE OWNED BY     O   ALTER SEQUENCE public.utilizador_iduser_seq OWNED BY public.utilizador.iduser;
          public          postgres    false    229            �
           2604    24103    consulta idconsulta    DEFAULT     z   ALTER TABLE ONLY public.consulta ALTER COLUMN idconsulta SET DEFAULT nextval('public.consulta_idconsulta_seq'::regclass);
 B   ALTER TABLE public.consulta ALTER COLUMN idconsulta DROP DEFAULT;
       public          postgres    false    204    203            �
           2604    24104    local idlocal    DEFAULT     n   ALTER TABLE ONLY public.local ALTER COLUMN idlocal SET DEFAULT nextval('public.local_idlocal_seq'::regclass);
 <   ALTER TABLE public.local ALTER COLUMN idlocal DROP DEFAULT;
       public          postgres    false    210    209            �
           2604    24105    pessoa idpessoa    DEFAULT     r   ALTER TABLE ONLY public.pessoa ALTER COLUMN idpessoa SET DEFAULT nextval('public.pessoa_idpessoa_seq'::regclass);
 >   ALTER TABLE public.pessoa ALTER COLUMN idpessoa DROP DEFAULT;
       public          postgres    false    213    212            �
           2604    24106    recibo idrecibo    DEFAULT     r   ALTER TABLE ONLY public.recibo ALTER COLUMN idrecibo SET DEFAULT nextval('public.recibo_idrecibo_seq'::regclass);
 >   ALTER TABLE public.recibo ALTER COLUMN idrecibo DROP DEFAULT;
       public          postgres    false    215    214            �
           2604    24107    role idrole    DEFAULT     j   ALTER TABLE ONLY public.role ALTER COLUMN idrole SET DEFAULT nextval('public.role_idrole_seq'::regclass);
 :   ALTER TABLE public.role ALTER COLUMN idrole DROP DEFAULT;
       public          postgres    false    217    216            �
           2604    24108    tipoconsulta idtipo    DEFAULT     z   ALTER TABLE ONLY public.tipoconsulta ALTER COLUMN idtipo SET DEFAULT nextval('public.tipoconsulta_idtipo_seq'::regclass);
 B   ALTER TABLE public.tipoconsulta ALTER COLUMN idtipo DROP DEFAULT;
       public          postgres    false    219    218            �
           2604    24109    tipoconvencao idtipoconv    DEFAULT     �   ALTER TABLE ONLY public.tipoconvencao ALTER COLUMN idtipoconv SET DEFAULT nextval('public.tipoconvencao_idtipoconv_seq'::regclass);
 G   ALTER TABLE public.tipoconvencao ALTER COLUMN idtipoconv DROP DEFAULT;
       public          postgres    false    221    220            �
           2604    24110    tipopermissao idtipopermissao    DEFAULT     �   ALTER TABLE ONLY public.tipopermissao ALTER COLUMN idtipopermissao SET DEFAULT nextval('public.tipopermissao_idtipopermissao_seq'::regclass);
 L   ALTER TABLE public.tipopermissao ALTER COLUMN idtipopermissao DROP DEFAULT;
       public          postgres    false    223    222            �
           2604    24111    tipotratamento idtipo    DEFAULT     ~   ALTER TABLE ONLY public.tipotratamento ALTER COLUMN idtipo SET DEFAULT nextval('public.tipotratamento_idtipo_seq'::regclass);
 D   ALTER TABLE public.tipotratamento ALTER COLUMN idtipo DROP DEFAULT;
       public          postgres    false    225    224            �
           2604    24112    utilizador iduser    DEFAULT     v   ALTER TABLE ONLY public.utilizador ALTER COLUMN iduser SET DEFAULT nextval('public.utilizador_iduser_seq'::regclass);
 @   ALTER TABLE public.utilizador ALTER COLUMN iduser DROP DEFAULT;
       public          postgres    false    229    228            �          0    24017    agenda 
   TABLE DATA           R   COPY public.agenda (utilizador_iduser, data, hora, local, idconsulta) FROM stdin;
    public          postgres    false    202   ܒ       �          0    24020    consulta 
   TABLE DATA           �   COPY public.consulta (idconsulta, dataconsulta, datamarcacao, descricao, estado, idtipoconvencao, pessoa_idutente, pessoa_idprofsaude, tipoconsulta_idtipo, local_idlocal) FROM stdin;
    public          postgres    false    203   ��       �          0    24025    consultalogs 
   TABLE DATA           e   COPY public.consultalogs (idpessoa, logdata, prevestado, newestado, consulta_idconsulta) FROM stdin;
    public          postgres    false    205   �       �          0    24028 	   convencao 
   TABLE DATA           g   COPY public.convencao (validade, identificacao, pessoa_idpessoa, tipoconvencao_idtipoconv) FROM stdin;
    public          postgres    false    206   3�       �          0    24031 
   documentos 
   TABLE DATA           S   COPY public.documentos (caminhoficheiro, fichaclinica_pessoa_idpessoa) FROM stdin;
    public          postgres    false    207   P�       �          0    24034    fichaclinica 
   TABLE DATA           B   COPY public.fichaclinica (descricao, pessoa_idpessoa) FROM stdin;
    public          postgres    false    208   m�       �          0    24037    local 
   TABLE DATA           H   COPY public.local (idlocal, morada, contacto, estado, nome) FROM stdin;
    public          postgres    false    209   ��       �          0    24045 
   permissoes 
   TABLE DATA           O   COPY public.permissoes (role_idrole, tipopermissaoidtipopermissao) FROM stdin;
    public          postgres    false    211   ��       �          0    24048    pessoa 
   TABLE DATA           m   COPY public.pessoa (idpessoa, nome, telefone, nif, morada, datanascimento, pessoa_idresponsavel) FROM stdin;
    public          postgres    false    212   ē       �          0    24056    recibo 
   TABLE DATA           g   COPY public.recibo (idrecibo, data, tipoconsulta, preco, estado, nif, consulta_idconsulta) FROM stdin;
    public          postgres    false    214   o�       �          0    24064    role 
   TABLE DATA           8   COPY public.role (idrole, nomerole, estado) FROM stdin;
    public          postgres    false    216   ��       �          0    24069    tipoconsulta 
   TABLE DATA           C   COPY public.tipoconsulta (idtipo, tipo, preco, estado) FROM stdin;
    public          postgres    false    218   ��       �          0    24074    tipoconvencao 
   TABLE DATA           O   COPY public.tipoconvencao (idtipoconv, tipo, descontoperc, estado) FROM stdin;
    public          postgres    false    220   є       �          0    24079    tipopermissao 
   TABLE DATA           P   COPY public.tipopermissao (idtipopermissao, textopermissao, estado) FROM stdin;
    public          postgres    false    222   �       �          0    24084    tipotratamento 
   TABLE DATA           E   COPY public.tipotratamento (idtipo, tipo, preco, estado) FROM stdin;
    public          postgres    false    224   �       �          0    24089 
   tratamento 
   TABLE DATA           b   COPY public.tratamento (descricao, preco, tipotratamento_idtipo, consulta_idconsulta) FROM stdin;
    public          postgres    false    226   (�       �          0    24092 	   userroles 
   TABLE DATA           C   COPY public.userroles (role_idrole, utilizador_iduser) FROM stdin;
    public          postgres    false    227   E�       �          0    24095 
   utilizador 
   TABLE DATA           n   COPY public.utilizador (iduser, username, password, email, estado, estadosessao, pessoa_idpessoa) FROM stdin;
    public          postgres    false    228   m�       �           0    0    consulta_idconsulta_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.consulta_idconsulta_seq', 1, false);
          public          postgres    false    204            �           0    0    local_idlocal_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.local_idlocal_seq', 1, false);
          public          postgres    false    210            �           0    0    pessoa_idpessoa_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.pessoa_idpessoa_seq', 10, true);
          public          postgres    false    213            �           0    0    recibo_idrecibo_seq    SEQUENCE SET     B   SELECT pg_catalog.setval('public.recibo_idrecibo_seq', 1, false);
          public          postgres    false    215            �           0    0    role_idrole_seq    SEQUENCE SET     =   SELECT pg_catalog.setval('public.role_idrole_seq', 1, true);
          public          postgres    false    217            �           0    0    tipoconsulta_idtipo_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.tipoconsulta_idtipo_seq', 1, false);
          public          postgres    false    219            �           0    0    tipoconvencao_idtipoconv_seq    SEQUENCE SET     K   SELECT pg_catalog.setval('public.tipoconvencao_idtipoconv_seq', 1, false);
          public          postgres    false    221            �           0    0 !   tipopermissao_idtipopermissao_seq    SEQUENCE SET     P   SELECT pg_catalog.setval('public.tipopermissao_idtipopermissao_seq', 1, false);
          public          postgres    false    223            �           0    0    tipotratamento_idtipo_seq    SEQUENCE SET     H   SELECT pg_catalog.setval('public.tipotratamento_idtipo_seq', 1, false);
          public          postgres    false    225            �           0    0    utilizador_iduser_seq    SEQUENCE SET     D   SELECT pg_catalog.setval('public.utilizador_iduser_seq', 10, true);
          public          postgres    false    229            �
           2606    24114    consulta consulta_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.consulta
    ADD CONSTRAINT consulta_pkey PRIMARY KEY (idconsulta);
 @   ALTER TABLE ONLY public.consulta DROP CONSTRAINT consulta_pkey;
       public            postgres    false    203            �
           2606    24116    fichaclinica fichaclinica_pkey 
   CONSTRAINT     i   ALTER TABLE ONLY public.fichaclinica
    ADD CONSTRAINT fichaclinica_pkey PRIMARY KEY (pessoa_idpessoa);
 H   ALTER TABLE ONLY public.fichaclinica DROP CONSTRAINT fichaclinica_pkey;
       public            postgres    false    208            �
           2606    24118    local local_pkey 
   CONSTRAINT     S   ALTER TABLE ONLY public.local
    ADD CONSTRAINT local_pkey PRIMARY KEY (idlocal);
 :   ALTER TABLE ONLY public.local DROP CONSTRAINT local_pkey;
       public            postgres    false    209            �
           2606    24120    pessoa pessoa_nif_key 
   CONSTRAINT     O   ALTER TABLE ONLY public.pessoa
    ADD CONSTRAINT pessoa_nif_key UNIQUE (nif);
 ?   ALTER TABLE ONLY public.pessoa DROP CONSTRAINT pessoa_nif_key;
       public            postgres    false    212            �
           2606    24122    pessoa pessoa_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.pessoa
    ADD CONSTRAINT pessoa_pkey PRIMARY KEY (idpessoa);
 <   ALTER TABLE ONLY public.pessoa DROP CONSTRAINT pessoa_pkey;
       public            postgres    false    212            �
           2606    24124    recibo recibo_pkey 
   CONSTRAINT     V   ALTER TABLE ONLY public.recibo
    ADD CONSTRAINT recibo_pkey PRIMARY KEY (idrecibo);
 <   ALTER TABLE ONLY public.recibo DROP CONSTRAINT recibo_pkey;
       public            postgres    false    214            �
           2606    24126    role role_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.role
    ADD CONSTRAINT role_pkey PRIMARY KEY (idrole);
 8   ALTER TABLE ONLY public.role DROP CONSTRAINT role_pkey;
       public            postgres    false    216            �
           2606    24128    tipoconsulta tipoconsulta_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.tipoconsulta
    ADD CONSTRAINT tipoconsulta_pkey PRIMARY KEY (idtipo);
 H   ALTER TABLE ONLY public.tipoconsulta DROP CONSTRAINT tipoconsulta_pkey;
       public            postgres    false    218            �
           2606    24130     tipoconvencao tipoconvencao_pkey 
   CONSTRAINT     f   ALTER TABLE ONLY public.tipoconvencao
    ADD CONSTRAINT tipoconvencao_pkey PRIMARY KEY (idtipoconv);
 J   ALTER TABLE ONLY public.tipoconvencao DROP CONSTRAINT tipoconvencao_pkey;
       public            postgres    false    220            �
           2606    24132     tipopermissao tipopermissao_pkey 
   CONSTRAINT     k   ALTER TABLE ONLY public.tipopermissao
    ADD CONSTRAINT tipopermissao_pkey PRIMARY KEY (idtipopermissao);
 J   ALTER TABLE ONLY public.tipopermissao DROP CONSTRAINT tipopermissao_pkey;
       public            postgres    false    222            �
           2606    24134 "   tipotratamento tipotratamento_pkey 
   CONSTRAINT     d   ALTER TABLE ONLY public.tipotratamento
    ADD CONSTRAINT tipotratamento_pkey PRIMARY KEY (idtipo);
 L   ALTER TABLE ONLY public.tipotratamento DROP CONSTRAINT tipotratamento_pkey;
       public            postgres    false    224            �
           2606    24136    utilizador utilizador_email_key 
   CONSTRAINT     [   ALTER TABLE ONLY public.utilizador
    ADD CONSTRAINT utilizador_email_key UNIQUE (email);
 I   ALTER TABLE ONLY public.utilizador DROP CONSTRAINT utilizador_email_key;
       public            postgres    false    228            �
           2606    24138    utilizador utilizador_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.utilizador
    ADD CONSTRAINT utilizador_pkey PRIMARY KEY (iduser);
 D   ALTER TABLE ONLY public.utilizador DROP CONSTRAINT utilizador_pkey;
       public            postgres    false    228                       2606    24140 "   utilizador utilizador_username_key 
   CONSTRAINT     a   ALTER TABLE ONLY public.utilizador
    ADD CONSTRAINT utilizador_username_key UNIQUE (username);
 L   ALTER TABLE ONLY public.utilizador DROP CONSTRAINT utilizador_username_key;
       public            postgres    false    228                       2606    24141    agenda fkagenda705986    FK CONSTRAINT     �   ALTER TABLE ONLY public.agenda
    ADD CONSTRAINT fkagenda705986 FOREIGN KEY (utilizador_iduser) REFERENCES public.utilizador(iduser);
 ?   ALTER TABLE ONLY public.agenda DROP CONSTRAINT fkagenda705986;
       public          postgres    false    228    2815    202                       2606    24146    consulta fkconsulta386388    FK CONSTRAINT     �   ALTER TABLE ONLY public.consulta
    ADD CONSTRAINT fkconsulta386388 FOREIGN KEY (local_idlocal) REFERENCES public.local(idlocal);
 C   ALTER TABLE ONLY public.consulta DROP CONSTRAINT fkconsulta386388;
       public          postgres    false    2795    203    209                       2606    24151    consulta fkconsulta685470    FK CONSTRAINT     �   ALTER TABLE ONLY public.consulta
    ADD CONSTRAINT fkconsulta685470 FOREIGN KEY (tipoconsulta_idtipo) REFERENCES public.tipoconsulta(idtipo);
 C   ALTER TABLE ONLY public.consulta DROP CONSTRAINT fkconsulta685470;
       public          postgres    false    218    2805    203                       2606    24156    consulta fkconsulta708468    FK CONSTRAINT     �   ALTER TABLE ONLY public.consulta
    ADD CONSTRAINT fkconsulta708468 FOREIGN KEY (pessoa_idutente) REFERENCES public.pessoa(idpessoa);
 C   ALTER TABLE ONLY public.consulta DROP CONSTRAINT fkconsulta708468;
       public          postgres    false    203    212    2799                       2606    24161    consulta fkconsulta871070    FK CONSTRAINT     �   ALTER TABLE ONLY public.consulta
    ADD CONSTRAINT fkconsulta871070 FOREIGN KEY (pessoa_idprofsaude) REFERENCES public.pessoa(idpessoa);
 C   ALTER TABLE ONLY public.consulta DROP CONSTRAINT fkconsulta871070;
       public          postgres    false    203    212    2799                       2606    24166    consultalogs fkconsultalo243268    FK CONSTRAINT     �   ALTER TABLE ONLY public.consultalogs
    ADD CONSTRAINT fkconsultalo243268 FOREIGN KEY (consulta_idconsulta) REFERENCES public.consulta(idconsulta);
 I   ALTER TABLE ONLY public.consultalogs DROP CONSTRAINT fkconsultalo243268;
       public          postgres    false    2791    205    203                       2606    24171    convencao fkconvencao115812    FK CONSTRAINT     �   ALTER TABLE ONLY public.convencao
    ADD CONSTRAINT fkconvencao115812 FOREIGN KEY (tipoconvencao_idtipoconv) REFERENCES public.tipoconvencao(idtipoconv);
 E   ALTER TABLE ONLY public.convencao DROP CONSTRAINT fkconvencao115812;
       public          postgres    false    220    206    2807            	           2606    24176    convencao fkconvencao609524    FK CONSTRAINT     �   ALTER TABLE ONLY public.convencao
    ADD CONSTRAINT fkconvencao609524 FOREIGN KEY (pessoa_idpessoa) REFERENCES public.pessoa(idpessoa);
 E   ALTER TABLE ONLY public.convencao DROP CONSTRAINT fkconvencao609524;
       public          postgres    false    212    206    2799            
           2606    24181    documentos fkdocumentos907172    FK CONSTRAINT     �   ALTER TABLE ONLY public.documentos
    ADD CONSTRAINT fkdocumentos907172 FOREIGN KEY (fichaclinica_pessoa_idpessoa) REFERENCES public.fichaclinica(pessoa_idpessoa);
 G   ALTER TABLE ONLY public.documentos DROP CONSTRAINT fkdocumentos907172;
       public          postgres    false    208    207    2793                       2606    24186    fichaclinica fkfichaclini474915    FK CONSTRAINT     �   ALTER TABLE ONLY public.fichaclinica
    ADD CONSTRAINT fkfichaclini474915 FOREIGN KEY (pessoa_idpessoa) REFERENCES public.pessoa(idpessoa);
 I   ALTER TABLE ONLY public.fichaclinica DROP CONSTRAINT fkfichaclini474915;
       public          postgres    false    212    208    2799                       2606    24191    permissoes fkpermissoes224375    FK CONSTRAINT     �   ALTER TABLE ONLY public.permissoes
    ADD CONSTRAINT fkpermissoes224375 FOREIGN KEY (tipopermissaoidtipopermissao) REFERENCES public.tipopermissao(idtipopermissao);
 G   ALTER TABLE ONLY public.permissoes DROP CONSTRAINT fkpermissoes224375;
       public          postgres    false    222    211    2809                       2606    24196    permissoes fkpermissoes783360    FK CONSTRAINT     �   ALTER TABLE ONLY public.permissoes
    ADD CONSTRAINT fkpermissoes783360 FOREIGN KEY (role_idrole) REFERENCES public.role(idrole);
 G   ALTER TABLE ONLY public.permissoes DROP CONSTRAINT fkpermissoes783360;
       public          postgres    false    216    2803    211                       2606    24201    pessoa fkpessoa771279    FK CONSTRAINT     �   ALTER TABLE ONLY public.pessoa
    ADD CONSTRAINT fkpessoa771279 FOREIGN KEY (pessoa_idresponsavel) REFERENCES public.pessoa(idpessoa);
 ?   ALTER TABLE ONLY public.pessoa DROP CONSTRAINT fkpessoa771279;
       public          postgres    false    212    212    2799                       2606    24206    recibo fkrecibo741225    FK CONSTRAINT     �   ALTER TABLE ONLY public.recibo
    ADD CONSTRAINT fkrecibo741225 FOREIGN KEY (consulta_idconsulta) REFERENCES public.consulta(idconsulta);
 ?   ALTER TABLE ONLY public.recibo DROP CONSTRAINT fkrecibo741225;
       public          postgres    false    214    203    2791                       2606    24211    tratamento fktratamento330575    FK CONSTRAINT     �   ALTER TABLE ONLY public.tratamento
    ADD CONSTRAINT fktratamento330575 FOREIGN KEY (tipotratamento_idtipo) REFERENCES public.tipotratamento(idtipo);
 G   ALTER TABLE ONLY public.tratamento DROP CONSTRAINT fktratamento330575;
       public          postgres    false    2811    226    224                       2606    24216    tratamento fktratamento891315    FK CONSTRAINT     �   ALTER TABLE ONLY public.tratamento
    ADD CONSTRAINT fktratamento891315 FOREIGN KEY (consulta_idconsulta) REFERENCES public.consulta(idconsulta);
 G   ALTER TABLE ONLY public.tratamento DROP CONSTRAINT fktratamento891315;
       public          postgres    false    203    226    2791                       2606    24221    userroles fkuserroles291355    FK CONSTRAINT     �   ALTER TABLE ONLY public.userroles
    ADD CONSTRAINT fkuserroles291355 FOREIGN KEY (role_idrole) REFERENCES public.role(idrole);
 E   ALTER TABLE ONLY public.userroles DROP CONSTRAINT fkuserroles291355;
       public          postgres    false    216    227    2803                       2606    24226    userroles fkuserroles57258    FK CONSTRAINT     �   ALTER TABLE ONLY public.userroles
    ADD CONSTRAINT fkuserroles57258 FOREIGN KEY (utilizador_iduser) REFERENCES public.utilizador(iduser);
 D   ALTER TABLE ONLY public.userroles DROP CONSTRAINT fkuserroles57258;
       public          postgres    false    228    227    2815                       2606    24231    utilizador fkutilizador520277    FK CONSTRAINT     �   ALTER TABLE ONLY public.utilizador
    ADD CONSTRAINT fkutilizador520277 FOREIGN KEY (pessoa_idpessoa) REFERENCES public.pessoa(idpessoa);
 G   ALTER TABLE ONLY public.utilizador DROP CONSTRAINT fkutilizador520277;
       public          postgres    false    2799    228    212            �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �   �   x�M�1
�0 ����@��'i�G'�AE;v��b5��"��sx1|�����Ի4��W���DN�,�&��X om�t%�k�7��(���U�CC��6Df�1� ��讜����o���+D�yS�9�*�[5>�_(�ީ4-�      �      x������ � �      �      x�3�,-I�+I�,����� '��      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x������ � �      �      x�3�4�2�bC�=... OD      �   g   x�m�1
�0��99LIA7��K�Q���V����?�k`�t�(��Z�U�I}�D��L�_fh�r>s�f�\�^��ҡ%`��-_�󵄣A��22     