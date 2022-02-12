import React, {useEffect, useState} from 'react';
import './style.css';
import * as THREE from "three";
import {FlyControls} from "three/examples/jsm/controls/FlyControls";
import {OrbitControls} from "three/examples/jsm/controls/OrbitControls";
import Footer from "./FooterGraphs";
import Minimap from "./Minimap";
import Header2 from "../Componente/PageConstructor/Header2";


function RenderGraph() {

    const {useRef, useEffect, useState} = React
    const mount = useRef(null)
    const [isAnimating, setAnimating] = useState(true)
    useRef(null);
    const tokenID = sessionStorage.getItem('token');
    const userTokenID = JSON.parse(tokenID);
    const tokenUsername = sessionStorage.getItem('userName');
    const userTokenUsername = JSON.parse(tokenUsername);
    const tokenEmail = sessionStorage.getItem('email');
    const userTokenEmail = JSON.parse(tokenEmail);
    let scene = new THREE.Scene();
    var mouse;
    var raycaster;

    const getInfo = async () => {
        const items = await fetch('api/RedesConexoes/'.concat(userTokenID) + '/niveis=0', {})
        return await items.json();
    }

    const getRedeConexoes = async () => {
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

        let paths = ["shortest", "strongest", "safest"];
        let possiblePaths = document.getElementById('possiblePaths');
        for (let j = 0; j < paths.length; j++) {
            let opt = paths[j];
            let el = document.createElement('option');
            el.text = opt;
            el.value = opt;
            possiblePaths.appendChild(el);
        }

        await fetch("/prolog/base_conhecimento");
    }

    function check() {
        let possiblePaths = document.getElementById('possiblePaths');
        let valueSafest = document.getElementById('valueSafest');
        valueSafest.readOnly = possiblePaths.value !== "safest";
        valueSafest.value = 0;
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

    async function teste() {
        let width = mount.current.clientWidth;
        let height = mount.current.clientHeight;
        let frameId;

        while (scene.children.length > 0) {
            scene.remove(scene.children[0]);
        }

        const camera = new THREE.PerspectiveCamera(75, width / height, 0.1, 1000);
        const renderer = new THREE.WebGLRenderer({alpha: true});

        let controls = new OrbitControls(camera, renderer.domElement);
        mouse = new THREE.Vector2();
        raycaster = new THREE.Raycaster();


        function configureControls() {
            controls.enablePan = true;
            controls.enableDamping = true;
            controls.enableRotate = false;
            controls.keys = {
                LEFT: 'ArrowLeft', //left arrow
                UP: 'ArrowUp', // up arrow
                RIGHT: 'ArrowRight', // right arrow
                BOTTOM: 'ArrowDown' // down arrow
            }
            controls.mouseButtons = {
                MIDDLE: THREE.MOUSE.DOLLY,
                RIGHT: THREE.MOUSE.PAN
            }

        }

        let radius = 1;
        const geometry = new THREE.CircleGeometry(radius, 100);
        let material, circle;
        // aqui vai ser para adicionar o user principal
        material = new THREE.MeshBasicMaterial({color: 'red', opacity: 1})
        circle = new THREE.Mesh(geometry, material);
        circle.position.x = 0;
        circle.position.y = 0;

        let spriteNamePrincipal = makeTextSprite(userTokenUsername);
        let spriteEmailPrincipal = makeTextSprite(userTokenEmail);

        spriteNamePrincipal.position.x = circle.position.x + 1.25;
        spriteNamePrincipal.position.y = circle.position.y - 0.6;
        spriteEmailPrincipal.position.x = circle.position.x + 0.8;
        spriteEmailPrincipal.position.y = circle.position.y - 0.8;

        scene.add(spriteNamePrincipal);
        scene.add(spriteEmailPrincipal);
        scene.add(circle);

        let raioAtual = radius;
        const diametroCirculo = radius * 2;
        const espacamento = radius + 0.3;

        let infoMap = await getInfo();
        let myMap = new Map(Object.entries(infoMap));

        let username_coordenadas = new Map();
        let id_username = new Map();

        let position = {
            positionx: 0,
            positiony: 0
        };
        username_coordenadas.set(userTokenID, position);
        id_username.set(userTokenID, userTokenUsername);

        for (let niveis = 1; niveis <= myMap.size; niveis++) {

            let tamanho = radius - (niveis / 10);

            if (tamanho < 0.2) {
                tamanho = 0.2;
            }

            let geometry = new THREE.CircleGeometry(tamanho, 100);

            let n = myMap.get('' + niveis).length; // numero de círculos para cada nível

            let tamanhoTotalNecessario = n * diametroCirculo + (espacamento * n); //Perimetro
            let raioMinimo = ((tamanhoTotalNecessario / Math.PI) / 2); // d = perimetro / pi

            if (raioMinimo <= raioAtual + diametroCirculo) {
                raioMinimo = raioAtual + diametroCirculo + espacamento;
            }

            for (let i = 0; i < n; i++) {

                let id = myMap.get('' + niveis)[i].secundario.id;

                let flag = true;

                if (username_coordenadas.get(id) == null) {
                    flag = false;

                    let spriteName = makeTextSprite(myMap.get('' + niveis)[i].secundario.username);
                    let spriteEmail = makeTextSprite(myMap.get('' + niveis)[i].secundario.email);

                    let x = (raioMinimo * Math.round(Math.cos((360 / n * i) * (Math.PI / 180))));
                    let y = (raioMinimo * Math.round(Math.sin((360 / n * i) * (Math.PI / 180))));

                    material = new THREE.MeshBasicMaterial({color: '#dbb587', opacity: 1});
                    circle = new THREE.Mesh(geometry, material);

                    if ((myMap.get('' + niveis)[i].secundario.username).length > 6) {
                        spriteName.position.x = (x + 1.05);
                        spriteEmail.position.x = (x + 0.6);
                    } else if ((myMap.get('' + niveis)[i].secundario.username).length <= 5) {
                        spriteName.position.x = (x + 1.25);
                        spriteEmail.position.x = (x + 0.8);

                    } else {
                        spriteName.position.x = (x + 1.2);
                        spriteEmail.position.x = (x + 0.73);
                    }

                    spriteName.position.y = (y - 0.6);
                    spriteEmail.position.y = (y - 0.8);

                    circle.position.x = x;
                    circle.position.y = y;

                    let position = {
                        nivel: niveis,
                        positionx: circle.position.x,
                        positiony: circle.position.y
                    };

                    scene.add(spriteName);
                    scene.add(spriteEmail);
                    scene.add(circle);
                    username_coordenadas.set(id, position);
                    id_username.set(id, myMap.get('' + niveis)[i].secundario.username);
                }

                //adicionar ligação
                let points = [];

                let userLigado = myMap.get('' + niveis)[i].principal.value;
                let positionUserLigado = username_coordenadas.get(userLigado);
                let positionXUserligado = positionUserLigado.positionx;
                let positionYUserligado = positionUserLigado.positiony;
                let tamanhoUserLigado = radius - ((niveis - 1) / 10);
                if (tamanhoUserLigado < 0.2) {
                    tamanhoUserLigado = 0.2;
                }

                let positionXUser;
                let positionYUser;
                let tamanhoUser;

                if (flag) {
                    let positionUser = username_coordenadas.get(id);
                    positionXUser = positionUser.positionx;
                    positionYUser = positionUser.positiony;
                    tamanhoUser = radius - ((positionUser.nivel) / 10);
                    if (tamanhoUser < 0.2) {
                        tamanhoUser = 0.2;
                    }
                } else {
                    positionXUser = circle.position.x;
                    positionYUser = circle.position.y;
                    tamanhoUser = tamanho;
                }

                //variação em x
                let pontoIntermedioX;
                let pontoIntermedioY;

                //variação em x negativa
                if (positionXUserligado !== positionXUser && positionYUserligado === positionYUser && positionXUserligado > positionXUser) {
                    pontoIntermedioX = ((positionXUserligado - tamanhoUserLigado) + (positionXUser + tamanhoUser)) / 2;
                    pontoIntermedioY = (positionYUserligado + positionYUser) / 2;
                    points = [new THREE.Vector3(positionXUserligado - tamanhoUserLigado, positionYUserligado, 0.0), new THREE.Vector3(positionXUser + tamanhoUser, positionYUser, 0.0)];
                    connectionSize(positionXUserligado - tamanhoUserLigado, positionYUserligado, positionXUser + tamanhoUser, positionYUser, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                }

                //variação em x positiva
                if (positionXUserligado !== positionXUser && positionYUserligado === positionYUser && positionXUserligado < positionXUser) {
                    pontoIntermedioX = ((positionXUserligado + tamanhoUserLigado) + (positionXUser - tamanhoUser)) / 2;
                    pontoIntermedioY = (positionYUserligado + positionYUser) / 2;
                    points = [new THREE.Vector3(positionXUserligado + tamanhoUserLigado, positionYUserligado, 0.0), new THREE.Vector3(positionXUser - tamanhoUser, positionYUser, 0.0)];
                    connectionSize(positionXUserligado + tamanhoUserLigado, positionYUserligado, positionXUser - tamanhoUser, positionYUser, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                }

                //variação em y

                //variação em y negativa
                if (positionYUserligado !== positionYUser && positionXUserligado === positionXUser && positionYUserligado > positionYUser) {
                    pontoIntermedioX = ((positionXUserligado) + (positionXUser)) / 2;
                    pontoIntermedioY = ((positionYUserligado - tamanhoUserLigado) + (positionYUser + tamanhoUser)) / 2;
                    points = [new THREE.Vector3(positionXUserligado, positionYUserligado - tamanhoUserLigado, 0.0), new THREE.Vector3(positionXUser, positionYUser + tamanhoUser, 0.0)];
                    connectionSize(positionXUserligado, positionYUserligado - tamanhoUserLigado, positionXUser, positionYUser + tamanhoUser, points, 2, myMap.get('' + niveis)[i].forcaLigacao);
                }

                //variação em y positiva
                if (positionYUserligado !== positionYUser && positionXUserligado === positionXUser && positionYUserligado < positionYUser) {
                    pontoIntermedioX = ((positionXUserligado) + (positionXUser)) / 2;
                    pontoIntermedioY = ((positionYUserligado + tamanhoUserLigado) + (positionYUser - tamanhoUser)) / 2;
                    points = [new THREE.Vector3(positionXUserligado, positionYUserligado + tamanhoUserLigado, 0.0), new THREE.Vector3(positionXUser, positionYUser - tamanhoUser, 0.0)];
                    connectionSize(positionXUserligado, positionYUserligado + tamanhoUserLigado, positionXUser, positionYUser - tamanhoUser, points, 2, myMap.get('' + niveis)[i].forcaLigacao);
                }

                //variação em x e em y
                if (positionYUserligado !== positionYUser && positionXUserligado !== positionXUser) {
                    let distanciaEntreDoisPontos = Math.sqrt(Math.pow(positionXUserligado - positionXUser, 2) + Math.pow(positionYUserligado - positionYUser, 2));
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
                        points = [new THREE.Vector3(positionXUserligado + x, positionYUserligado + y, 0.0), new THREE.Vector3(positionXUserligado + x1, positionYUserligado + y1, 0.0)];
                        connectionSize(positionXUserligado + x, positionYUserligado + y, positionXUserligado + x1, positionYUserligado + y1, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                    }

                    // x < e y >
                    if (positionYUser > positionYUserligado && positionXUser < positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado - x) + (positionXUserligado - x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado + y) + (positionYUserligado + y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado - x, positionYUserligado + y, 0.0), new THREE.Vector3(positionXUserligado - x1, positionYUserligado + y1, 0.0)];
                        connectionSize(positionXUserligado - x, positionYUserligado + y, positionXUserligado - x1, positionYUserligado + y1, points, 3, myMap.get('' + niveis)[i].forcaLigacao);
                    }

                    // x < e y <
                    if (positionYUser < positionYUserligado && positionXUser < positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado - x) + (positionXUserligado - x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado - y) + (positionYUserligado + y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado - x, positionYUserligado - y, 0.0), new THREE.Vector3(positionXUserligado - x1, positionYUserligado - y1, 0.0)];
                        connectionSize(positionXUserligado - x, positionYUserligado - y, positionXUserligado - x1, positionYUserligado - y1, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                    }

                    // x > e y <
                    if (positionYUser < positionYUserligado && positionXUser > positionXUserligado) {
                        pontoIntermedioX = ((positionXUserligado + x) + (positionXUserligado + x1)) / 2;
                        pontoIntermedioY = ((positionYUserligado - y) + (positionYUserligado - y1)) / 2;
                        points = [new THREE.Vector3(positionXUserligado + x, positionYUserligado - y, 0.0), new THREE.Vector3(positionXUserligado + x1, positionYUserligado - y1, 0.0)];
                        connectionSize(positionXUserligado + x, positionYUserligado - y, positionXUserligado + x1, positionYUserligado - y1, points, 1, myMap.get('' + niveis)[i].forcaLigacao);
                    }
                }

                let geometryLine = new THREE.BufferGeometry().setFromPoints(points);
                let materialLine = new THREE.LineBasicMaterial({color: 'OrangeRed'});
                let line = new THREE.LineSegments(geometryLine, materialLine);
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
                    let spriteTags = makeTextSprite(text);
                    spriteTags.position.x = pontoIntermedioX + radius;
                    spriteTags.position.y = pontoIntermedioY - (radius / 2);
                    scene.add(spriteTags);
                }
            }

            raioAtual = raioMinimo;
        }

        function connectionSize(x1, y1, x2, y2, points, option, strength) {
            for (let i = 0; i < strength * 7; i++) {
                if (option === 1) {
                    points.push(new THREE.Vector3(x1, y1 + (0.0001 * i), 0.0), new THREE.Vector3(x2, y2 + (0.0001 * i), 0.0));
                }
                if (option === 2) {
                    points.push(new THREE.Vector3(x1 + (0.0001 * i), y1, 0.0), new THREE.Vector3(x2 + (0.0001 * i), y2, 0.0));
                }
                if (option === 3) {
                    points.push(new THREE.Vector3(x1 + (0.0001 * i), y1 + (0.0001 * i), 0.0), new THREE.Vector3(x2 + (0.0001 * i), y2 + (0.0001 * i), 0.0));
                }
            }
        }


        function makeTextSprite(message, parameters) {
            if (parameters === undefined) parameters = {};
            var fontface = parameters.hasOwnProperty("fontface") ? parameters["fontface"] : "Courier New";
            var fontsize = parameters.hasOwnProperty("fontsize") ? parameters["fontsize"] : 15;
            var borderThickness = parameters.hasOwnProperty("borderThickness") ? parameters["borderThickness"] : 4;
            var borderColor = parameters.hasOwnProperty("borderColor") ? parameters["borderColor"] : {
                r: 0,
                g: 0,
                b: 0,
                a: 1.0
            };
            var backgroundColor = parameters.hasOwnProperty("backgroundColor") ? parameters["backgroundColor"] : {
                r: 0,
                g: 0,
                b: 255,
                a: 1.0
            };
            var textColor = parameters.hasOwnProperty("textColor") ? parameters["textColor"] : {
                r: 0,
                g: 0,
                b: 0,
                a: 1.0
            };

            var canvas = document.createElement('canvas');
            var context = canvas.getContext('2d');
            context.font = "Bold " + fontsize + "px " + fontface;
            context.measureText(message);
            context.fillStyle = "rgba(" + backgroundColor.r + "," + backgroundColor.g + "," + backgroundColor.b + "," + backgroundColor.a + ")";
            context.strokeStyle = "rgba(" + borderColor.r + "," + borderColor.g + "," + borderColor.b + "," + borderColor.a + ")";
            context.fillStyle = "rgba(" + textColor.r + ", " + textColor.g + ", " + textColor.b + ", 1.0)";
            context.fillText(message, borderThickness, fontsize + borderThickness);

            var texture = new THREE.Texture(canvas)
            texture.needsUpdate = true;
            var spriteMaterial = new THREE.SpriteMaterial({map: texture, useScreenCoordinates: false});
            var sprite = new THREE.Sprite(spriteMaterial);
            sprite.scale.set(0.2 * fontsize, 0.1 * fontsize);
            return sprite;
        }

        camera.position.z = 6
        renderer.setClearColor('white')
        renderer.setSize(width, height)

        const renderScene = () => {
            let SCREEN_W, SCREEN_H;
            SCREEN_W = window.innerWidth;
            SCREEN_H = window.innerHeight;

            let left, bottom, width, height;


            left = 10;
            bottom = 0;
            width = 100 + SCREEN_W;
            height = SCREEN_H;
            renderer.setViewport(left, bottom, width, height);
            renderer.render(scene, camera);


        }

        const handleResize = () => {
            width = mount.current.clientWidth
            height = mount.current.clientHeight
            renderer.setSize(width, height)

            camera.aspect = width / height
            camera.updateProjectionMatrix()
            renderScene()
        }

        const animate = () => {

            renderScene()
            configureControls()
            frameId = window.requestAnimationFrame(animate)

        }

        const start = () => {
            if (!frameId) {
                frameId = requestAnimationFrame(animate)
            }
        }

        const stop = () => {
            cancelAnimationFrame(frameId)
            frameId = null
        }

        mount.current.appendChild(renderer.domElement)
        window.addEventListener('resize', handleResize)
        start()

        controls.current = {start, stop}
        FlyControls.current = {start, stop}

        return () => {
            stop()
            window.removeEventListener('resize', handleResize)
            mount.current.removeChild(renderer.domElement)

            scene.remove(circle)
            geometry.dispose()
            material.dispose()
        }

    }


    useEffect(async () => {
        getRedeConexoes();
        teste();
    }, [])
    return (
        <div>
            <Header2/>
            <div className="vis" ref={mount} onClick={() => setAnimating(!isAnimating)}>
                <div/>
            </div>
            <div className="minimap"><Minimap/></div>
            <div className="pagePaths">
                <div className="PathsSpace">
                    <div className="possiblePathsSpace">
                        <span>Select The Path Type:   </span>
                        <select id='possiblePaths' onChange={check}>
                        </select>
                    </div>
                    <div className="possibleUsersSpace">
                        <span>Select One User:   </span>
                        <select id='possibleUsers'>
                        </select>
                    </div>
                    <div className="textSafest">
                        <span className="me-4" id="textSafest">Minimum Connection Strength:  </span>
                        <input className="valueSpace" readOnly="true" id="valueSafest" type="number" min="-100"
                               max="100" defaultValue="0"/>
                    </div>
                </div>
                <div className="btnPaths">
                    <button onClick={getPath}>GET PATH</button>
                </div>
                <div className="pathText2">
                    <p id="pathText">

                    </p>
                </div>
            </div>
            <Footer/>
        </div>
    )
}

export default RenderGraph;