import * as THREE from "three";
import React from "react";
import Footer from "./Footer3DGraph";
import './Footer.css';
import {OrbitControls} from "three/examples/jsm/controls/OrbitControls";
import Minimap from "../Graph/Minimap";
import ImagemAlegre from "../EmojisEstadosHumor/EmojiAlegre.png";
import ImagemAliviado from "../EmojisEstadosHumor/EmojiAliviado.png";
import ImagemAngustiado from "../EmojisEstadosHumor/EmojiAngustiado.png";
import ImagemComRemorsos from "../EmojisEstadosHumor/EmojiComRemorsos.png";
import ImagemDesapontado from "../EmojisEstadosHumor/EmojiDesapontado.png";
import ImagemEsperancoso from "../EmojisEstadosHumor/EmojiEsperançoso.png";
import ImagemGrato from "../EmojisEstadosHumor/EmojiGrato.png";
import ImagemMedroso from "../EmojisEstadosHumor/EmojiMedroso.png";
import ImagemNervoso from "../EmojisEstadosHumor/EmojiNervoso.png";
import ImagemOrgulhoso from "../EmojisEstadosHumor/EmojiOrgulhoso.png";
import Header2 from "../Componente/PageConstructor/Header2";
import SpriteText from 'three-spritetext';
import {GLTFLoader} from "three/examples/jsm/loaders/GLTFLoader";

function RenderGraph3D() {

    const {useRef, useEffect, useState} = React
    const mount = useRef(null)
    const [isAnimating] = useState(true)
    useRef(null);
    const tokenID = sessionStorage.getItem('token');
    const userTokenID = JSON.parse(tokenID);
    const tokenUsername = sessionStorage.getItem('userName');
    const userTokenUsername = JSON.parse(tokenUsername);
    let scene = new THREE.Scene();
    let isHoverSame = "";
    let tip = null;
    let display = false;
    let id_tags = new Map();
    let pikachu = null;
    let gengar = null;
    let lucario = null;
    let squirtle = null;
    let eevee = null;
    let blastoise = null;
    let charizard = null;
    let darkrai = null;
    let mewtwo = null;
    let pokeball = null;
    let magikarp = null;
    let controls;
    let camera;
    let flashlight;
    const listener = new THREE.AudioListener();
    const sound = new THREE.Audio(listener);
    const audioLoader = new THREE.AudioLoader();
    let haveInformation = false;
    const raycaster = new THREE.Raycaster();
    const mouse = new THREE.Vector2();
    const renderer = new THREE.WebGLRenderer();

    const getInfo = async () => {
        const items = await fetch('api/RedesConexoes/'.concat(userTokenID) + '/niveis=0', {})
        return await items.json();
    }

    useEffect(() => {
        let loader = new GLTFLoader().setPath('/avatars/squirtle/');
        loader.load('scene.gltf', function (gltf) {
            squirtle = gltf.scene;
            squirtle.scale.set(3, 3, 3);
        });

        loader = new GLTFLoader().setPath('/avatars/pikachu/');
        loader.load('scene.gltf', function (gltf) {
            pikachu = gltf.scene;
            pikachu.scale.set(0.5, 0.5, 0.5);
        });

        loader = new GLTFLoader().setPath('/avatars/gengar/');
        loader.load('scene.gltf', function (gltf) {
            gengar = gltf.scene;
        });

        loader = new GLTFLoader().setPath('/avatars/lucario/');
        loader.load('scene.gltf', function (gltf) {
            lucario = gltf.scene;
            lucario.scale.set(3, 3, 3);
        });

        loader = new GLTFLoader().setPath('/avatars/eevee/');
        loader.load('scene.gltf', function (gltf) {
            eevee = gltf.scene;
            eevee.scale.set(0.1, 0.1, 0.1);
        });

        loader = new GLTFLoader().setPath('/avatars/blastoise/');
        loader.load('scene.gltf', function (gltf) {
            blastoise = gltf.scene;
            blastoise.scale.set(0.3, 0.3, 0.3);
        });

        loader = new GLTFLoader().setPath('/avatars/charizard/');
        loader.load('scene.gltf', function (gltf) {
            charizard = gltf.scene;
            charizard.scale.set(0.25, 0.25, 0.25);
        });

        loader = new GLTFLoader().setPath('/avatars/darkrai/');
        loader.load('scene.gltf', function (gltf) {
            darkrai = gltf.scene;
            darkrai.scale.set(0.3, 0.3, 0.3);
        });

        loader = new GLTFLoader().setPath('/avatars/mewtwo/');
        loader.load('scene.gltf', function (gltf) {
            mewtwo = gltf.scene;
            mewtwo.scale.set(0.3, 0.3, 0.3);
        });

        loader = new GLTFLoader().setPath('/avatars/pokeball/');
        loader.load('scene.gltf', function (gltf) {
            pokeball = gltf.scene;
            pokeball.scale.set(2.5, 2.5, 2.5);
        });

        loader = new GLTFLoader().setPath('/avatars/magikarp/');
        loader.load('scene.gltf', function (gltf) {
            magikarp = gltf.scene;
            magikarp.scale.set(0.5, 0.5, 0.5);
        });
    }, [])

    useEffect(async () => {
        haveInformation = false;

        let infoMap = await getInfo();
        let myMap = new Map(Object.entries(infoMap));

        for (let niveis = 1; niveis <= myMap.size; niveis++) {
            for (let i = 0; i < myMap.get('' + niveis).length; i++) {
                let id = myMap.get('' + niveis)[i].secundario.id;
                if (id_tags.get(id) == null) {
                    id_tags.set(id, myMap.get('' + niveis)[i].listaTagsSecundario)
                }
            }
        }
        haveInformation = true;
    }, [isAnimating])

    useEffect(async () => {
        let info;
        const tokenID = sessionStorage.getItem('token');
        const userTokenID = JSON.parse(tokenID);
        await fetch('api/RedesConexoes/'.concat(userTokenID) + '/niveis=0')
            .then(async res => info = await res.json());

        let myMap = new Map(Object.entries(info));
        let usernames = [];

        for (let niveis = 1; niveis <= myMap.size; niveis++) {
            for (let i = 0; i < myMap.get('' + niveis).length; i++) {
                let username = myMap.get('' + niveis)[i].secundario.username;
                if (!usernames.includes(username)) {
                    usernames.push(username);
                }
            }
        }

        let possibleUsers = document.getElementById('possibleUsers');
        for (let i = 0; i < usernames.length; i++) {
            let opt = usernames[i];
            let el = document.createElement('option');
            el.text = opt;
            el.value = opt;
            possibleUsers.appendChild(el);
        }

        let paths = ["shortest", "strongest", "safest", "strongestWithEmotion"];
        let possiblePaths = document.getElementById('possiblePaths');
        for (let j = 0; j < paths.length; j++) {
            let opt = paths[j];
            let el = document.createElement('option');
            el.text = opt;
            el.value = opt;
            possiblePaths.appendChild(el);
        }

        await fetch("/prolog/base_conhecimento");
    }, [])

    function check() {
        let possiblePaths = document.getElementById('possiblePaths');
        let valueSafest = document.getElementById('valueSafest');
        valueSafest.readOnly = possiblePaths.value !== "safest";
        valueSafest.value = 0;
    }

    async function getEmoji(emojiUser, x, y, z) {
        let image, x1, y1, z1;
        switch (emojiUser.description) {
            case "Joy":
                image = ImagemAlegre;
                x1 = 4;
                y1 = 4;
                z1 = 4;
                break;
            case "Distress":
                image = ImagemAngustiado;
                x1 = 3;
                y1 = 3;
                z1 = 3;
                break;
            case "Hope":
                image = ImagemEsperancoso;
                x1 = 3;
                y1 = 3;
                z1 = 3;
                break;
            case "Fear":
                image = ImagemMedroso;
                x1 = 3;
                y1 = 3;
                z1 = 3;
                break;
            case "Relief":
                image = ImagemAliviado;
                x1 = 3;
                y1 = 3;
                z1 = 3;
                break;
            case "Disappointment":
                image = ImagemDesapontado;
                x1 = 2.8;
                y1 = 2.8;
                z1 = 2.8;
                break;
            case "Pride":
                image = ImagemOrgulhoso;
                x1 = 4.5;
                y1 = 3;
                z1 = 3;
                break;
            case "Remorse":
                image = ImagemComRemorsos;
                x1 = 2.8;
                y1 = 2.8;
                z1 = 2.8;
                break;
            case "Gratitude":
                image = ImagemGrato;
                x1 = 4;
                y1 = 2.8;
                z1 = 2.8;
                break;
            case "Anger":
                image = ImagemNervoso;
                x1 = 4;
                y1 = 2.8;
                z1 = 2.8;
                break;
            default:
                image = ImagemAlegre;
                x1 = 4;
                y1 = 4;
                z1 = 4;
        }
        const img = new THREE.SpriteMaterial({
            map: THREE.ImageUtils.loadTexture(image),

        });
        img.map.needsUpdate = true; //ADDED

        const spriteEmoji = new THREE.Sprite(img);
        spriteEmoji.scale.set(x1, y1, z1);
        spriteEmoji.position.x = x + 1;
        spriteEmoji.position.y = (y - 4);
        spriteEmoji.position.z = z;

        scene.add(spriteEmoji);
    }

    function music() {
        const button = document.getElementById("soundButton")

        if (sound.isPlaying) {
            sound.stop();
            button.innerText = "SOUND ON"
        } else {
            sound.play();
            button.innerText = "SOUND OFF"
        }
    }

    function addControls() {

        const button = document.getElementById("viewButton");
        var text = "" + button.innerText;
        console.log(text);

        let width = mount.current.clientWidth
        let height = mount.current.clientHeight

        if (text.slice(-1) === 'N') {
            camera = new THREE.PerspectiveCamera(50, width / height, 1, 100);
            camera.position.z = 10;
            //controls = new FlyControls(camera, renderer.domElement);

            camera.add(flashlight);
            flashlight.target = camera;
            scene.add(flashlight.target);

            /*controls.movementSpeed = 10;
            controls.rollSpeed = 0.2;
            controls.dragToLook = false;
            controls.autoForward = false;*/

            button.innerText = "First Person Mode: OFF";
            display = true;
        }

        if (text.slice(-1) === 'F') {
            camera = new THREE.PerspectiveCamera(50, width / height, 1, 1000);
            camera.position.z = 70;
            controls = new OrbitControls(camera, renderer.domElement);

            camera.add(flashlight);
            flashlight.target = camera;
            scene.add(flashlight.target);

            button.innerText = "First Person Mode: ON";
            display = false;
        }

    }

    async function getPath() {
        const tokenUserName = sessionStorage.getItem('userName');
        const userTokenUserName = JSON.parse(tokenUserName);

        let possiblePaths = document.getElementById('possiblePaths');
        let possibleUsers = document.getElementById('possibleUsers');
        let valueSafest = document.getElementById('valueSafest');
        let pathText = document.getElementById('pathText');

        let path;

        if (possiblePaths.value === "shortest") {
            await fetch("/prolog/caminhoMaisCurto?nome=" + userTokenUserName + "&nome1=" + possibleUsers.value)
                .then(async res => path = await res.json());
        } else if (possiblePaths.value === "safest") {
            await fetch("/prolog/caminho_mais_seguro?from=" + userTokenUserName + "&to=" + possibleUsers.value + "&value=" + valueSafest.value)
                .then(async res => path = await res.json());
        } else if (possiblePaths.value === "strongest") {
            await fetch("/prolog/caminho_mais_forte?nome1=" + userTokenUserName + "&nome2=" + possibleUsers.value)
                .then(async res => path = await res.json());
        } else if (possiblePaths.value === "strongestWithEmotion") {
            await fetch("/prolog/caminhoMaisForteComEmocoes?from=" + userTokenUserName + "&to=" + possibleUsers.value + "&value=20")
                .then(async res => path = await res.json());
        }

        let pathString = "";

        if (path.length === 0) {
            pathString = "No Path Found!"
        } else {
            for (let i = 0; i < path.length; i++) {
                if (i !== (path.length - 1)) {
                    pathString = pathString + path[i] + " -> ";
                } else {
                    pathString = pathString + path[i];
                }
            }
        }
        pathText.innerText = pathString;

        const aux = pathString.split(" -> ");
        const ligacoes = scene.children.filter(obj => String(obj.name).includes(","));
        for (let y = 0; y < ligacoes.length; y++) {
            const ligacao = scene.getObjectByName(ligacoes[y].name);
            ligacao.material.color.set("OrangeRed");
        }
        for (let b = 0; b < aux.length - 1; b++) {
            const nome = "" + aux[b] + "," + aux[b + 1];
            const ligacao = scene.getObjectByName(nome);
            ligacao.material.color.set("DeepSkyBlue");
        }
    }

    useEffect(async () => {
        let width = mount.current.clientWidth
        let height = mount.current.clientHeight
        //let frameId

        while (scene.children.length > 0) {
            scene.remove(scene.children[0]);
        }

        scene = new THREE.Scene();

        camera = new THREE.PerspectiveCamera(50, width / height, 1, 4000);
        renderer.setSize(window.innerWidth, window.innerHeight);
        document.body.appendChild(renderer.domElement);

        let mood;
        await fetch("api/EstadosDeHumor/estadoHumorUser/" + userTokenID)
            .then(async res => mood = await res.json());

        const loader = new THREE.TextureLoader();
        let urlBack = 'background/' + mood.description + '.jpg';
        loader.load(urlBack, function (texture) {
            scene.background = texture;
        });

        camera.add(listener);

        let url = 'sounds/' + mood.description + '.mp3';

        audioLoader.load(url, function (buffer) {
            sound.setBuffer(buffer);
            sound.setLoop(true);
            sound.setVolume(0.5);
        });

        //AMBIENT LIGHTS
        const light = new THREE.AmbientLight(0xFFFF8A); // soft yellow light
        scene.add(light);

        //FIXED LIGHTS
        const pointLight = new THREE.PointLight(0xffffff, 0.3, 5000);
        pointLight.position.set(500, 0, 800);
        scene.add(pointLight);

        const pointLight2 = new THREE.PointLight(0xffffff, 0.3, 5000);
        pointLight2.position.set(-500, 0, -800);
        scene.add(pointLight2);

        //SPOTLIGHT
        flashlight = new THREE.SpotLight(0xffffff, 0.5, 1000);
        camera.add(flashlight);
        flashlight.position.set(0, 0, 1);
        flashlight.target = camera;
        scene.add(flashlight.target);

        controls = new OrbitControls(camera, renderer.domElement);
        //configureControls();

        let radius = 2.5;
        const geometry = new THREE.SphereGeometry(radius, 32, 16);

        let material, sphere;
        material = new THREE.MeshStandardMaterial({metalness: 0.5, roughness: 0.5, color: 0xff0000});
        sphere = new THREE.Mesh(geometry, material);

        sphere.position.x = 0;
        sphere.position.y = 0;
        sphere.position.z = 0;

        let spriteNamePrincipal = new SpriteText('You', 1);
        spriteNamePrincipal.color = 'white';
        spriteNamePrincipal.backgroundColor = 'black';
        spriteNamePrincipal.borderColor = 'black';
        spriteNamePrincipal.borderWidth = 2;
        spriteNamePrincipal.borderRadius = 6;
        spriteNamePrincipal.position.x = sphere.position.x;
        spriteNamePrincipal.position.y = sphere.position.y + 4;
        spriteNamePrincipal.position.z = sphere.position.z;
        spriteNamePrincipal.translateZ(0.7);
        spriteNamePrincipal.scale.set(2, 2, 2);

        scene.add(spriteNamePrincipal);
        sphere.name = "You";
        scene.add(sphere);

        let raioAtual = radius;

        const diametroCirculo = radius * 2;
        const espacamento = radius + 0.3;

        let infoMap = await getInfo();
        let myMap = new Map(Object.entries(infoMap));

        let id_coordenadas = new Map();
        let user_coordenadas = new Map();
        let id_username = new Map();

        let position = {
            positionx: 0,
            positiony: 0,
            positionz: 0
        };
        id_coordenadas.set(userTokenID, position);
        user_coordenadas.set(userTokenUsername, position);
        id_username.set(userTokenID, userTokenUsername);

        for (let niveis = 1; niveis <= myMap.size; niveis++) {
            let tamanho = radius - (niveis / 10);

            if (tamanho < 0.2) {
                tamanho = 0.2;
            }

            let geometry = new THREE.SphereGeometry(radius, 32, 16);

            let n = myMap.get('' + niveis).length;

            let tamanhoTotalNecessario = n * diametroCirculo + (espacamento * n);
            let raioMinimo = ((tamanhoTotalNecessario / Math.PI) / 2);

            if (raioMinimo <= raioAtual + diametroCirculo) {
                raioMinimo = raioAtual + diametroCirculo + espacamento;
            }

            for (let i = 0; i < n; i++) {

                let id = myMap.get('' + niveis)[i].secundario.id;

                let flag = true;

                if (id_coordenadas.get(id) == null) {
                    flag = false;

                    let x = (raioMinimo * Math.round(Math.cos((360 / n * i) * (Math.PI / 180))));
                    let y = (raioMinimo * Math.round(Math.sin((360 / n * i) * (Math.PI / 180))));
                    let z = getRndInteger(-10, 10);

                    material = new THREE.MeshStandardMaterial({metalness: 0.5, roughness: 0.5, color: 0xdbb587});
                    sphere = new THREE.Mesh(geometry, material);
                    sphere.name = myMap.get('' + niveis)[i].secundario.id;

                    sphere.position.x = x;
                    sphere.position.y = y;
                    sphere.position.z = z;

                    let position = {
                        nivel: niveis,
                        positionx: sphere.position.x,
                        positiony: sphere.position.y,
                        positionz: sphere.position.z
                    };

                    scene.add(sphere);
                    id_coordenadas.set(id, position);
                    user_coordenadas.set(myMap.get('' + niveis)[i].secundario.username, position);
                    id_username.set(id, myMap.get('' + niveis)[i].secundario.username);

                    let emoji;
                    await fetch("api/EstadosDeHumor/estadoHumorUser/" + id)
                        .then(async res => emoji = await res.json());

                    await getEmoji(emoji, x - 1, y, z);
                }

                //adicionar ligação
                let points = [];

                let userLigado = myMap.get('' + niveis)[i].principal.value;
                let positionUserLigado = id_coordenadas.get(userLigado);
                let positionXUserligado = positionUserLigado.positionx;
                let positionYUserligado = positionUserLigado.positiony;
                let positionZUserligado = positionUserLigado.positionz;
                let tamanhoUserLigado = radius - ((niveis - 1) / 10);
                if (tamanhoUserLigado < 0.2) {
                    tamanhoUserLigado = 0.2;
                }

                let positionXUser;
                let positionYUser;
                let positionZUser;
                let tamanhoUser;

                if (flag) {
                    let positionUser = id_coordenadas.get(id);
                    positionXUser = positionUser.positionx;
                    positionYUser = positionUser.positiony;
                    positionZUser = positionUser.positionz;
                    tamanhoUser = radius - ((positionUser.nivel) / 10);
                    if (tamanhoUser < 0.2) {
                        tamanhoUser = 0.2;
                    }
                } else {
                    positionXUser = sphere.position.x;
                    positionYUser = sphere.position.y;
                    positionZUser = sphere.position.z;
                    tamanhoUser = tamanho;
                }

                //variação em x
                let pontoIntermedioX;
                let pontoIntermedioY;

                //variação em x negativa
                if (positionXUserligado !== positionXUser && positionYUserligado === positionYUser && positionXUserligado > positionXUser) {
                    pontoIntermedioX = ((positionXUserligado - tamanhoUserLigado) + (positionXUser + tamanhoUser)) / 2;
                    pontoIntermedioY = (positionYUserligado + positionYUser) / 2;
                    points = [new THREE.Vector3(positionXUserligado - tamanhoUserLigado, positionYUserligado, positionZUserligado), new THREE.Vector3(positionXUser + tamanhoUser, positionYUser, positionZUser)];
                }

                //variação em x positiva
                if (positionXUserligado !== positionXUser && positionYUserligado === positionYUser && positionXUserligado < positionXUser) {
                    pontoIntermedioX = ((positionXUserligado + tamanhoUserLigado) + (positionXUser - tamanhoUser)) / 2;
                    pontoIntermedioY = (positionYUserligado + positionYUser) / 2;
                    points = [new THREE.Vector3(positionXUserligado + tamanhoUserLigado, positionYUserligado, positionZUserligado), new THREE.Vector3(positionXUser - tamanhoUser, positionYUser, positionZUser)];
                }

                //variação em y
                //variação em y negativa
                if (positionYUserligado !== positionYUser && positionXUserligado === positionXUser && positionYUserligado > positionYUser) {
                    pontoIntermedioX = ((positionXUserligado) + (positionXUser)) / 2;
                    pontoIntermedioY = ((positionYUserligado - tamanhoUserLigado) + (positionYUser + tamanhoUser)) / 2;
                    points = [new THREE.Vector3(positionXUserligado, positionYUserligado - tamanhoUserLigado, positionZUserligado), new THREE.Vector3(positionXUser, positionYUser + tamanhoUser, positionZUser)];
                }

                //variação em y positiva
                if (positionYUserligado !== positionYUser && positionXUserligado === positionXUser && positionYUserligado < positionYUser) {
                    pontoIntermedioX = ((positionXUserligado) + (positionXUser)) / 2;
                    pontoIntermedioY = ((positionYUserligado + tamanhoUserLigado) + (positionYUser - tamanhoUser)) / 2;
                    points = [new THREE.Vector3(positionXUserligado, positionYUserligado + tamanhoUserLigado, positionZUserligado), new THREE.Vector3(positionXUser, positionYUser - tamanhoUser, positionZUser)];
                }

                //variação em x e em y
                if (positionYUserligado !== positionYUser && positionXUserligado !== positionXUser) {
                    let distanciaEntreDoisPontos = Math.sqrt(Math.pow(positionXUserligado - positionXUser, 2) + Math.pow(positionYUserligado - positionYUser, 2) + Math.pow(positionZUserligado - positionZUser, 2));
                    let valorTeoremaTales = distanciaEntreDoisPontos / tamanhoUserLigado;
                    let x = (Math.abs(positionXUser - positionXUserligado) / valorTeoremaTales);
                    let y = (Math.abs(positionYUser - positionYUserligado) / valorTeoremaTales);
                    let distanciaEntreDoisPontos2 = distanciaEntreDoisPontos - tamanhoUser;
                    let valorTeoremaTales2 = distanciaEntreDoisPontos2 / tamanhoUserLigado;
                    let x1 = x * valorTeoremaTales2;
                    let y1 = y * valorTeoremaTales2;
                    // x > e y >
                    if (positionYUser > positionYUserligado && positionXUser > positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado + x) + (positionXUserligado + x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado + y) + (positionYUserligado + y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado + x, positionYUserligado + y, positionZUserligado), new THREE.Vector3(positionXUserligado + x1, positionYUserligado + y1, positionZUser)];
                    }

                    // x < e y >
                    if (positionYUser > positionYUserligado && positionXUser < positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado - x) + (positionXUserligado - x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado + y) + (positionYUserligado + y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado - x, positionYUserligado + y, positionZUserligado), new THREE.Vector3(positionXUserligado - x1, positionYUserligado + y1, positionZUser)];
                    }

                    // x < e y <
                    if (positionYUser < positionYUserligado && positionXUser < positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado - x) + (positionXUserligado - x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado - y) + (positionYUserligado + y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado - x, positionYUserligado - y, positionZUserligado), new THREE.Vector3(positionXUserligado - x1, positionYUserligado - y1, positionZUser)];
                    }

                    // x > e y <
                    if (positionYUser < positionYUserligado && positionXUser > positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado + x) + (positionXUserligado + x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado - y) + (positionYUserligado - y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado + x, positionYUserligado - y, positionZUserligado), new THREE.Vector3(positionXUserligado + x1, positionYUserligado - y1, positionZUser)];
                    }
                }

                let pontoIntermedioZ = (positionZUserligado + positionZUser) / 2;

                let forcaLigacao = myMap.get('' + niveis)[i].forcaLigacao;
                let radiusTube = forcaLigacao / 100;

                let tubeGeometry = new THREE.TubeGeometry(
                    new THREE.CatmullRomCurve3(points),
                    1000,// path segments
                    radiusTube,// THICKNESS
                    8, //Roundness of Tube
                    false //closed
                );

                let materialLine = new THREE.LineBasicMaterial({color: 'OrangeRed'});
                let line = new THREE.Line(tubeGeometry, materialLine);
                const nomePrincipal = id_username.get(myMap.get('' + niveis)[i].principal.value);
                line.name = "" + nomePrincipal + "," + myMap.get('' + niveis)[i].secundario.username;
                scene.add(line);

                //tags nas ligações de nivel 1
                if (niveis === 1) {
                    let text = "[";
                    let array = myMap.get('' + niveis)[i].listaTags;
                    for (let k = 0; k < array.length; k++) {
                        text = text + array[k].nome;
                        if (k !== (array.length - 1)) {
                            text = text + ",";
                        }
                    }
                    text = text + "]";
                    let spriteTags = new SpriteText(text, 1);
                    spriteTags.color = 'white';
                    spriteTags.backgroundColor = 'black';
                    spriteTags.borderColor = 'black';
                    spriteTags.borderWidth = 2;
                    spriteTags.borderRadius = 6;
                    spriteTags.position.x = pontoIntermedioX;
                    spriteTags.position.y = pontoIntermedioY + 2;
                    spriteTags.position.z = pontoIntermedioZ - (radius / 2);
                    spriteTags.translateZ(0.7);
                    spriteTags.scale.set(4, 3, 3);
                    scene.add(spriteTags);
                }
            }

            raioAtual = raioMinimo;
        }

        const renderScene = () => {
            raycaster.setFromCamera(mouse, camera);
            // calculate objects intersecting the picking ray
            const intersects = raycaster.intersectObjects(scene.children)
            let esferas = intersects.filter(obj => String(obj.object.name).length === 36);

            if (esferas.length !== 0) {
                let id = esferas[0].object.name;
                if (isHoverSame !== id) {
                    if (haveInformation) {
                        let last = isHoverSame.charAt(isHoverSame.length - 1);
                        switch (last) {
                            case "0":
                                scene.remove(gengar);
                                break;
                            case "1":
                                scene.remove(lucario);
                                break;
                            case "2":
                                scene.remove(pikachu);
                                break;
                            case "3":
                                scene.remove(squirtle);
                                break;
                            case "4":
                                scene.remove(eevee);
                                break;
                            case "5":
                                scene.remove(blastoise);
                                break;
                            case "6":
                                scene.remove(charizard);
                                break;
                            case "7":
                                scene.remove(darkrai);
                                break;
                            case "8":
                                scene.remove(mewtwo);
                                break;
                            case "9":
                                scene.remove(magikarp);
                                break;
                            default:
                                scene.remove(pokeball);
                                break;
                        }
                        let name = id_username.get(id);
                        let tagsUser = id_tags.get(id);
                        let text = name + "\n" + "[";
                        for (let k = 0; k < tagsUser.length; k++) {
                            text = text + tagsUser[k].nome;
                            if (k !== (tagsUser.length - 1)) {
                                text = text + ",";
                            }
                        }
                        scene.remove(tip)
                        isHoverSame = id;
                        text = text + "]";
                        tip = new SpriteText(text, 0.2);
                        let user = id_coordenadas.get(id);
                        tip.color = 'white';
                        tip.backgroundColor = 'black';
                        tip.borderColor = 'black';
                        tip.borderWidth = 2;
                        tip.borderRadius = 6;
                        tip.position.x = user.positionx;
                        tip.position.y = (user.positiony + 11);
                        tip.position.z = user.positionz;
                        tip.translateZ(0.7);
                        scene.add(tip)

                        //add avatar
                        last = isHoverSame.charAt(isHoverSame.length - 1);

                        switch (last) {
                            case "0":
                                gengar.position.x = user.positionx;
                                gengar.position.y = (user.positiony + 5);
                                gengar.position.z = user.positionz;
                                scene.add(gengar);
                                break;
                            case "1":
                                lucario.position.x = user.positionx;
                                lucario.position.y = (user.positiony + 2.50);
                                lucario.position.z = user.positionz;
                                scene.add(lucario);
                                break;
                            case "2":
                                pikachu.position.x = user.positionx;
                                pikachu.position.y = (user.positiony + 3);
                                pikachu.position.z = user.positionz;
                                scene.add(pikachu);
                                break;
                            case "3":
                                squirtle.position.x = user.positionx;
                                squirtle.position.y = (user.positiony + 5.5);
                                squirtle.position.z = user.positionz;
                                scene.add(squirtle);
                                break;
                            case "4":
                                eevee.position.x = user.positionx;
                                eevee.position.y = (user.positiony + 3);
                                eevee.position.z = user.positionz;
                                scene.add(eevee);
                                break;
                            case "5":
                                blastoise.position.x = user.positionx;
                                blastoise.position.y = (user.positiony + 3);
                                blastoise.position.z = user.positionz;
                                scene.add(blastoise);
                                break;
                            case "6":
                                charizard.position.x = user.positionx;
                                charizard.position.y = (user.positiony + 3);
                                charizard.position.z = user.positionz;
                                scene.add(charizard);
                                break;
                            case "7":
                                darkrai.position.x = user.positionx;
                                darkrai.position.y = (user.positiony + 3);
                                darkrai.position.z = user.positionz;
                                scene.add(darkrai);
                                break;
                            case "8":
                                mewtwo.position.x = user.positionx;
                                mewtwo.position.y = (user.positiony + 3);
                                mewtwo.position.z = user.positionz;
                                scene.add(mewtwo);
                                break;
                            case "9":
                                magikarp.position.x = user.positionx;
                                magikarp.position.y = (user.positiony + 4.9);
                                magikarp.position.z = user.positionz;
                                scene.add(magikarp);
                                break;
                            default:
                                pokeball.position.x = user.positionx;
                                pokeball.position.y = (user.positiony + 4.9);
                                pokeball.position.z = user.positionz;
                                scene.add(pokeball);
                                break;
                        }
                    }
                }
            } else {
                scene.remove(tip)
                const last = isHoverSame.charAt(isHoverSame.length - 1);
                isHoverSame = "";
                //remove avatar
                switch (last) {
                    case "0":
                        scene.remove(gengar);
                        break;
                    case "1":
                        scene.remove(lucario);
                        break;
                    case "2":
                        scene.remove(pikachu);
                        break;
                    case "3":
                        scene.remove(squirtle);
                        break;
                    case "4":
                        scene.remove(eevee);
                        break;
                    case "5":
                        scene.remove(blastoise);
                        break;
                    case "6":
                        scene.remove(charizard);
                        break;
                    case "7":
                        scene.remove(darkrai);
                        break;
                    case "8":
                        scene.remove(mewtwo);
                        break;
                    case "9":
                        scene.remove(magikarp);
                        break;
                    default:
                        scene.remove(pokeball);
                        break;
                }
            }

            renderer.render(scene, camera);
        }

        camera.position.z = 70;
        renderer.setClearColor('white')
        renderer.setSize(width, height)

        const handleResize = () => {
            width = mount.current.clientWidth
            height = mount.current.clientHeight
            renderer.setSize(width, height)

            camera.aspect = width / height
            camera.updateProjectionMatrix()
            renderScene();
        }

        const animate = () => {
            requestAnimationFrame(animate);
            renderScene();
            controls.update(0.05);
        }

        mount.current.appendChild(renderer.domElement)
        window.addEventListener('resize', handleResize)
        animate();
        window.addEventListener('mousemove', onMouseMove, false);
        window.addEventListener('keypress', onKeyPress, false);
        window.addEventListener('keydown', onKeyDown, false);

        function onKeyPress(event) {
            let eventKey = event.key;
            let keyCode = event.keyCode;
            let walkingSpeed = 10;
            let turningSpeed = 0.2;

            if (display) {
                const deltaT = 0.1;
                let coveredDistance = walkingSpeed * deltaT;
                let directionIncrement = turningSpeed * deltaT;
                if (eventKey === "d") {
                    camera.rotation.y += directionIncrement;
                } else if (eventKey === "a") {
                    camera.rotation.y -= directionIncrement;
                }
                let vector = new THREE.Vector3();
                camera.getWorldDirection(vector);
                let theta = Math.atan2(vector.x, vector.z);
                const direction = THREE.MathUtils.degToRad(theta);
                if (eventKey === "s") {
                    let cameraPosition = camera.position.clone();
                    if (!collision(cameraPosition.add(new THREE.Vector3(coveredDistance * Math.sin(direction), 0, coveredDistance * Math.cos(direction))))) {
                        camera.translateX(coveredDistance * Math.sin(direction));
                        camera.translateZ(coveredDistance * Math.cos(direction));
                    }
                } else if (eventKey === "w") {
                    let cameraPosition = camera.position.clone();
                    if (!collision(cameraPosition.add(new THREE.Vector3(-coveredDistance * Math.sin(direction), 0, -coveredDistance * Math.cos(direction))))) {
                        camera.translateX(-coveredDistance * Math.sin(direction));
                        camera.translateZ(-coveredDistance * Math.cos(direction));
                    }
                }
                if (keyCode === 38) {
                    console.log("up")
                    /*let cameraPosition = this.camera3D.position.clone();
                    if (
                        !this.collision(
                            cameraPosition.add(new THREE.Vector3(0, coveredDistance, 0))
                        )
                    ) {
                        this.camera3D.translateY(coveredDistance);
                    }*/
                } else if (keyCode === 40) {
                    console.log("down")
                    /*let cameraPosition = this.camera3D.position.clone();
                    if (
                        !this.collision(
                            cameraPosition.add(new THREE.Vector3(0, -coveredDistance, 0))
                        )
                    ) {
                        this.camera3D.translateY(-coveredDistance);
                    }*/
                }
            }
        }

        function onKeyDown(event) {
            let keyCode = event.keyCode;
            let walkingSpeed = 10;

            if (display) {
                const deltaT = 0.1;
                let coveredDistance = walkingSpeed * deltaT;
                if (keyCode === 40) {
                    //down
                    let cameraPosition = camera.position.clone();
                    if (!collision(cameraPosition.add(new THREE.Vector3(0, coveredDistance, 0)))) {
                        camera.translateY(coveredDistance);
                    }
                } else if (keyCode === 38) {
                    //up
                    let cameraPosition = camera.position.clone();
                    if (!collision(cameraPosition.add(new THREE.Vector3(0, -coveredDistance, 0)))) {
                        camera.translateY(-coveredDistance);
                    }
                }
                if (keyCode === 37) {
                    //left
                    let cameraPosition = camera.position.clone();
                    if (!collision(cameraPosition.add(new THREE.Vector3(0, coveredDistance, 0)))) {
                        camera.translateX(coveredDistance);
                    }
                } else if (keyCode === 39) {
                    //right
                    let cameraPosition = camera.position.clone();
                    if (!collision(cameraPosition.add(new THREE.Vector3(0, -coveredDistance, 0)))) {
                        camera.translateX(-coveredDistance);
                    }
                }
            }
        }

        function collision(position) {
            for (let i = 0; i < scene.children.length; i++) {
                if (String(scene.children[i].name).length === 36 || String(scene.children[i].name) === "You") {
                    try {
                        if ((scene.children[i].position.distanceTo(position)) <= (scene.children[i].geometry.parameters.radius + 1.4)) {
                            return true;
                        }
                    } catch {
                    }
                } else if (String(scene.children[i].name).split(",").length === 2) {
                    let user1 = String(scene.children[i].name).split(",")[0];
                    let user2 = String(scene.children[i].name).split(",")[1];
                    let positionAux1 = user_coordenadas.get(user1);
                    let positionAux = user_coordenadas.get(user2);
                    let position1 = {
                        x: positionAux1.positionx,
                        y: positionAux1.positiony,
                        z: positionAux1.positionz
                    };
                    let position2 = {
                        x: positionAux.positionx,
                        y: positionAux.positiony,
                        z: positionAux.positionz
                    };
                    const target = new THREE.Vector3();
                    const line = new THREE.Line3(position1, position2);
                    const d = line.closestPointToPoint(position, true, target).distanceTo(position);
                    if (d <= scene.children[i].geometry.parameters.radius + 0.8) {
                        return true;
                    }
                }
            }
            return false;
        }

        return () => {
            window.removeEventListener('resize', handleResize)
            mount.current.removeChild(renderer.domElement)

            scene.remove(sphere)
            geometry.dispose()
            material.dispose()
        }
    }, [isAnimating])

    return (
        <div style={{position: "relative"}}>
            <Header2/>
            <div className="pagePaths" style={{backgroundColor: 'transparent', zIndex: "100", position: "absolute"}}>
                <div className="PathsSpace">
                    <div className="possiblePathsSpace">
                        <span style={{color: "white", backgroundColor: "black"}}>Select The Path Type:   </span>
                        <select id='possiblePaths' onChange={check}>
                        </select>
                    </div>
                    <div className="possibleUsersSpace">
                        <span style={{color: "white", backgroundColor: "black"}}>Select One User:   </span>
                        <select id='possibleUsers'>
                        </select>
                    </div>
                    <div className="textSafest">
                        <span className="me-4" id="textSafest" style={{color: "white", backgroundColor: "black"}}>Minimum Connection Strength:  </span>
                        <input className="valueSpace" readOnly="true" id="valueSafest" type="number" min="-100"
                               max="100" defaultValue="0"/>
                    </div>
                </div>
                <div className="btnPaths mt-3">
                    <button id="viewButton" onClick={addControls}> First Person Mode: ON</button>
                    <button onClick={getPath}>GET PATH</button>
                    <button id="soundButton" onClick={music}>SOUND ON</button>
                </div>
                <div className="pathText2">
                    <p id="pathText">

                    </p>
                </div>
            </div>
            <div className="vis3" ref={mount}>
            </div>
            <div><Minimap/></div>
            <Footer/>
        </div>
    )

    function getRndInteger(min, max) {
        return Math.floor(Math.random() * (max - min)) + min;
    }

    function onMouseMove(event) {

        mouse.x = (event.clientX / window.innerWidth) * 2 - 1;
        mouse.y = -(event.clientY / window.innerHeight) * 2 + 1;

        if (mouse.y < -0.65) {
            mouse.y = mouse.y;
        } else if (mouse.y > -0.05) {
            mouse.y = mouse.y + 0.17;
        } else {
            mouse.y = -(event.clientY / window.innerHeight) * 2 + 1 + 0.07;
        }
    }
}

export default RenderGraph3D;